using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Reflection;
using System.Text.Json;

using E5Renewer;
using E5Renewer.Controllers;
using E5Renewer.Models;
using E5Renewer.Models.BackgroundServices;
using E5Renewer.Models.Config;
using E5Renewer.Models.GraphAPIs;
using E5Renewer.Models.Modules;
using E5Renewer.Models.Statistics;

using Microsoft.AspNetCore.Mvc;

/// <inheritdoc/>
public static class Program
{
    private const string timeStampFormat = "yyyy-MM-dd HH:mm:ss ";
    private static readonly List<IModulesChecker> modulesCheckers = new();
    private static readonly List<IConfigParser> configParsers = new();
    private static readonly List<IAspNetModule> aspNetModules = new();
    private static ILogger logger = null!;

    /// <summary>Start E5Renewer.</summary>
    /// <param name="config">The path to config file.</param>
    /// <param name="systemd">If program runs in systemd environment.</param>
    public static async Task Main(FileInfo config, bool systemd = false)
    {
        string env = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Production";
        LogLevel minimumLevel = env == "Debug" ? LogLevel.Debug : LogLevel.Information;
        Program.logger = LoggerFactory.Create(
            (configure) =>
            {
                configure.ClearProviders();
                if (systemd)
                {
                    configure.AddSystemdConsole((config) => config.TimestampFormat = Program.timeStampFormat);
                }
                else
                {
                    configure.AddSimpleConsole((config) =>
                    {
                        config.SingleLine = true;
                        config.TimestampFormat = Program.timeStampFormat;
                    });
                }
                configure.SetMinimumLevel(minimumLevel);
            }
        ).CreateLogger(nameof(Program));

        Program.modulesCheckers.Clear();
        Program.configParsers.Clear();
        Program.aspNetModules.Clear();

        Program.DiscoverModules(Assembly.GetExecutingAssembly());
        IEnumerable<Assembly> assemblies = GetPossibleModulesPaths().
        Select(
            (directory) =>
            {
                FileInfo[] files = directory.GetFiles(directory.Name + ".dll", SearchOption.TopDirectoryOnly);
                if (files.Count() > 0)
                {
                    FileInfo file = files[0];
                    ModuleLoadContext context = new(file);
                    string assemblyName = Path.GetFileNameWithoutExtension(file.FullName);
                    try
                    {
                        Assembly assembly = context.LoadFromAssemblyName(
                            new(assemblyName)
                        );
                        Program.logger.LogDebug("Load assembly from {0} success.", assemblyName);
                        return assembly;
                    }
                    catch (Exception e)
                    {
                        Program.logger.LogError("Failed to load assembly because {0}.", e.Message);
                    }
                }
                return null;
            }
        ).OfType<Assembly>();
        Program.DiscoverModules(assemblies.ToArray());

        Program.CheckModules();

        await Program.LaunchServerAsync(await Program.ParseConfigAsync(config), systemd);
    }

    private static void CheckModules()
    {
        foreach (IConfigParser parser in Program.configParsers)
        {
            foreach (IModulesChecker checker in Program.modulesCheckers)
            {
                Program.logger.LogDebug("Checking module {0} with checker {1}...", parser.name, checker.name);
                checker.CheckModules(parser);
            }
        }
    }

    private static async ValueTask<Config> ParseConfigAsync(FileInfo config)
    {
        Config runtimeConfig;
        try
        {
            IConfigParser parser = Program.configParsers.First((parser) => parser.IsSupported(config));
            runtimeConfig = await parser.ParseConfigAsync(config);
        }
        catch (InvalidOperationException)
        {
            Program.logger.LogError("Failed to find parser for config {0}.", config);
            throw;
        }
        catch (Exception e)
        {
            Program.logger.LogError("Failed to parse config {0} because {1}.", config, e.Message);
            throw;
        }
        return runtimeConfig;
    }

    private static async ValueTask LaunchServerAsync(Config config, bool systemd)
    {
        if (!config.isCheckPassed)
        {
            throw new InvalidDataException("config check failed.");
        }
        bool setSocketPermission = false;
        WebApplicationBuilder builder = WebApplication.CreateBuilder();
        builder.Logging.ClearProviders();
        if (systemd)
        {
            builder.Logging.AddSystemdConsole((config) => config.TimestampFormat = Program.timeStampFormat);
        }
        else
        {
            builder.Logging.AddSimpleConsole(
                (config) =>
                {
                    config.SingleLine = true; config.TimestampFormat = Program.timeStampFormat;
                }
            );
        }
        builder.WebHost.ConfigureKestrel(
            (configure) =>
            {
                const uint minPort = 1;
                const uint maxPort = 65535;

                bool portValid =
                    config.listenPort >= minPort &&
                    config.listenPort <= maxPort &&
                    !Program.IsPortUsed(config.listenPort);

                bool listenHttp =
                    IPAddress.TryParse(config.listenAddr, out IPAddress? listenAddress) &&
                    portValid;
                if (listenHttp && listenAddress is not null)
                {
                    configure.Listen(
                        listenAddress,
                        (int)config.listenPort
                    );
                }
                else if (Socket.OSSupportsUnixDomainSockets)
                {
                    configure.ListenUnixSocket(config.listenSocket);
                    setSocketPermission = true;
                }
                else
                {
                    throw new Exception("Cannot Bind to HTTP or Unix Domain Socket.");
                }
            }
        );

        builder.Services.ApplyRuntimeConfig(config);

        WebApplication app = builder.Build();
        app.UseExceptionHandler(
            (exceptionHandlerApp) => exceptionHandlerApp.Run(
                async (context) =>
                    {
                        InvokeResult result = await GenerateDummyResult(context);
                        await context.Response.WriteAsJsonAsync(result);
                    }
            )
        );
        app.UseRouting();
        string[] allowedMethods = ["GET", "POST"];
        app.Logger.LogDebug("Setting allowed method to {0}", string.Join(", ", allowedMethods));
        app.UseHttpMethodChecker(allowedMethods);
        app.Logger.LogDebug("Setting check Unix timestamp in request");
        app.UseUnixTimestampChecker();
        app.Logger.LogDebug("Setting authToken");
        app.UseAuthTokenAuthentication(config.authToken);
        app.Logger.LogDebug("Mapping controllers");
        app.MapControllers();
        await app.StartAsync();
        if (setSocketPermission && !OperatingSystem.IsWindows())
        {
            File.SetUnixFileMode(config.listenSocket, config.listenSocketPermission.ToUnixFileMode());
        }
        await app.WaitForShutdownAsync();
    }

    private static IServiceCollection ApplyRuntimeConfig(this IServiceCollection services, Config config)
    {
        if (config.isCheckPassed)
        {
            if (config.passwords is not null)
            {
                services.AddSingleton(config.passwords);
            }
            services.AddSingleton<IStatusManager, MemoryStatusManager>();
            services.AddSingleton<IUnixTimestampGenerator, UnixTimestampGenerator>();
            services.AddSingleton<ICertificatePasswordProvider, ConfigCertificatePasswordProvider>();
            services.AddHostedService<PrepareUsersService>();
            services.AddHostedService<ModulesCheckerService>();
            foreach (IModulesChecker checker in Program.modulesCheckers)
            {
                services.AddSingleton<IModulesChecker>(checker);
            }
            Random random = new();
            IEnumerable<IGraphAPICaller> graphAPICallers = Program.aspNetModules.OfType<IGraphAPICaller>();
            IEnumerable<IAspNetModule> otherAspNetModules = Program.aspNetModules.Where((module) => (module as IGraphAPICaller) is null);
            foreach (GraphUser user in config.users)
            {
                services.AddSingleton<GraphUser>(user);
                IGraphAPICaller caller = random.GetItems(graphAPICallers.ToArray(), 1)[0];
                services.AddKeyedSingleton(user, caller);
            }
            foreach (IAspNetModule module in otherAspNetModules)
            {
                services.AddSingleton(module);
            }
            IEnumerable<Type> apiFunctionsTypes = Assembly.GetExecutingAssembly().GetTypes().GetNonAbstractClassesAssainableTo<IAPIFunction>();
            foreach (Type t in apiFunctionsTypes)
            {
                logger.LogDebug("Registering {0} as {1}", t.FullName, nameof(IAPIFunction));
                services.AddSingleton(typeof(IAPIFunction), t);
            }
        }

        services.AddControllers(
        ).AddJsonOptions(
              (options) =>
                {
                    options.JsonSerializerOptions.WriteIndented = true;
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
                }
        ).ConfigureApiBehaviorOptions(
            (options) =>
            {
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    InvokeResult result = Program.GenerateDummyResult(actionContext.HttpContext).Result;
                    return new JsonResult(result);
                };
            }
        );
        return services;
    }

    private static void DiscoverModules(params Assembly[] assemblies)
    {
        foreach (Assembly assembly in assemblies)
        {
            foreach (Type t in Program.GetModules(assembly))
            {
                IModule? instance = null;
                try
                {
                    instance = Activator.CreateInstance(t) as IModule;
                }
                catch (Exception e)
                {
                    Program.logger.LogError("Failed to create instance for module {0} because {1}.", t.FullName, e.Message);
                }

                bool discovered = false;
                if (instance is IModulesChecker moduleChecker)
                {
                    Program.logger.LogDebug(
                        "Found modules checker {0} with name {1}.",
                        moduleChecker.GetType().FullName,
                        moduleChecker.name
                    );
                    modulesCheckers.Add(moduleChecker);
                    discovered = true;
                }

                if (instance is IConfigParser configParser)
                {
                    Program.logger.LogDebug(
                        "Found config parser {0} with name {1}.",
                        configParser.GetType().FullName,
                        configParser.name
                    );
                    configParsers.Add(configParser);
                    discovered = true;
                }

                if (instance is IAspNetModule aspNetModule)
                {
                    Program.logger.LogDebug(
                        "Found aspnet module {0} with name {1}.",
                        aspNetModule.GetType().FullName,
                        aspNetModule.name
                    );
                    aspNetModules.Add(aspNetModule);
                    discovered = true;
                }

                if (!discovered && instance is not null)
                {
                    Program.logger.LogWarning(
                        "Found unknown module {0} with name {1}, ignoring",
                        instance.GetType().FullName,
                            instance.name);
                }
            }
        }
    }

    private static IEnumerable<Type> GetModules(Assembly assembly)
    {
        return assembly.GetTypes().GetNonAbstractClassesAssainableTo<IModule>().Where(
            (t) => t.IsDefined(typeof(ModuleAttribute))
        );
    }

    private static IEnumerable<DirectoryInfo> GetPossibleModulesPaths()
    {
        const string modulesBaseFolderName = "modules";
        const string modulesBaseFileName = "E5Renewer.Modules.*.dll";
        Dictionary<string, IEnumerable<DirectoryInfo>> results = new();
        DirectoryInfo assemblyDirectory = new(Path.Combine(AppContext.BaseDirectory, modulesBaseFolderName));
        DirectoryInfo[] directoriesToCheck = [assemblyDirectory];
        foreach (DirectoryInfo currentDirectory in directoriesToCheck)
        {
            if (currentDirectory.Exists && !results.ContainsKey(currentDirectory.FullName))
            {
                // /modules/*/E5Renewer.Modules.*/E5Renewer.Modules.*.dll
                IEnumerable<DirectoryInfo> directories = currentDirectory.GetFiles(modulesBaseFileName, SearchOption.AllDirectories).Where(
                    (fileInfo) => (fileInfo.Directory?.Name ?? string.Empty) == fileInfo.Name.Substring(0, fileInfo.Name.Length - 4)
                ).Select((x) => x.Directory).OfType<DirectoryInfo>();
                results[currentDirectory.FullName] = directories;
            }
        }
        return results.Values.SelectMany((x) => x);
    }
    private static bool IsPortUsed(uint port)
    {
        IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
        TcpConnectionInformation[] tcpConnInfoArray = ipGlobalProperties.GetActiveTcpConnections();
        foreach (TcpConnectionInformation info in tcpConnInfoArray)
        {
            if (info.LocalEndPoint.Port == port)
            {
                return true;
            }
        }
        return false;
    }
    private static async ValueTask<InvokeResult> GenerateDummyResult(HttpContext context)
    {
        IUnixTimestampGenerator unixTimestampGenerator = context.RequestServices.GetRequiredService<IUnixTimestampGenerator>();
        Dictionary<string, object?> queries;
        switch (context.Request.Method)
        {
            case "GET":
                queries = context.Request.Query.Select(
                    (kv) => new KeyValuePair<string, object?>(kv.Key, kv.Value.FirstOrDefault() as object)
                ).ToDictionary();
                break;
            case "POST":
                byte[] buffer = new byte[context.Request.ContentLength ?? context.Request.Body.Length];
                int length = await context.Request.Body.ReadAsync(buffer);
                byte[] contents = buffer.Take(length).ToArray();
                queries = JsonSerializer.Deserialize<Dictionary<string, object?>>(contents) ?? new();
                break;
            default:
                queries = new();
                break;
        }
        if (queries.ContainsKey("timestamp"))
        {
            queries.Remove("timestamp");
        }
        string fullPath = context.Request.PathBase + context.Request.Path;
        int lastOfSlash = fullPath.LastIndexOf("/");
        int firstOfQuote = fullPath.IndexOf("?");
        string methodName =
            firstOfQuote > lastOfSlash ?
                fullPath.Substring(lastOfSlash + 1, firstOfQuote - lastOfSlash) :
                fullPath.Substring(lastOfSlash + 1);
        return new InvokeResult(
                methodName,
                queries,
                null,
                unixTimestampGenerator.GetUnixTimestamp()
            );
    }
}
