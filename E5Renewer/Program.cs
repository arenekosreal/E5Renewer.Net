using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Hosting;
using System.CommandLine.NamingConventionBinder;
using System.CommandLine.Parsing;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Reflection;
using System.Text.Json;

using E5Renewer;
using E5Renewer.Controllers;
using E5Renewer.Models;
using E5Renewer.Models.BackgroundServices;
using E5Renewer.Models.CommandLine;
using E5Renewer.Models.Config;
using E5Renewer.Models.GraphAPIs;
using E5Renewer.Models.Modules;
using E5Renewer.Models.Statistics;

using Microsoft.AspNetCore.Mvc;

const string timeStampFormat = "yyyy-MM-dd HH:mm:ss ";

RootCommand rootCommand = new("Renew E5 Subscription by calling msgraph apis.")
{
    new Option<FileInfo>(["--config", "-c"],"The path to config file.")
    {
        IsRequired = true
    },
    new Option<bool>("--systemd", "If run this program in systemd environment.")
};

rootCommand.Handler = CommandHandler.Create<CommandLineParsedResult, IHost>(
    async (result, host) =>
    {
        ILogger logger = host.Services.GetRequiredService<ILoggerFactory>().CreateLogger("RootCommand.Handler");
        IEnumerable<IModulesChecker> modulesCheckers = host.Services.GetServices<IModulesChecker>();
        IEnumerable<IConfigParser> configParsers = host.Services.GetServices<IConfigParser>();
        IEnumerable<Type> moduleTypesToAspNet = host.Services.GetServices<List<Type>>()?.SelectMany((i) => i) ?? new List<Type>();
        logger.LogDebug("Get {0} module(s)", modulesCheckers.Count() + configParsers.Count());
        logger.LogDebug("Send {0} module(s) to AspNet.Core: {1}", moduleTypesToAspNet.Count(), moduleTypesToAspNet);
        List<IModule> modulesToCheck = new();
        modulesToCheck.AddRange(modulesCheckers);
        modulesToCheck.AddRange(configParsers);
        foreach (IModule module in modulesToCheck)
        {
            foreach (IModulesChecker checker in modulesCheckers)
            {
                logger.LogDebug("Checking module {0} with checker {1}", module.name, checker.name);
                checker.CheckModules(module);
            }
        }
        Config config;
        if (configParsers.Any((i) => i.IsSupported(result.config)))
        {
            IConfigParser configParser = configParsers.First((i) => i.IsSupported(result.config));
            config = await configParser.ParseConfigAsync(result.config);
            logger.LogDebug("Parsing config with parser {0}", configParser.name);
        }
        else
        {
            throw new ArgumentException(
                string.Format(
                    "Unable to find a parser for config {0}. Only {1}found.",
                    result.config.FullName,
                    string.Join(", ", configParsers.Select((x) => x.name))
                )
            );
        }

        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        builder.Logging.ClearProviders();
        if (result.systemd)
        {
            builder.Logging.AddSystemdConsole(
                (config) => config.TimestampFormat = timeStampFormat
            );
        }
        else
        {
            builder.Logging.AddSimpleConsole(
                (config) =>
                {
                    config.SingleLine = true;
                    config.TimestampFormat = timeStampFormat;
                }
            );
        }

        builder.WebHost.ConfigureKestrel(
            (configure) =>
            {
                const uint maxPort = 65535;

                bool listenHttp =
                    !string.IsNullOrEmpty(config.listenAddr) &&
                    config.listenPort > 0 &&
                    config.listenPort <= maxPort &&
                    !IsPortUsed(config.listenPort);
                if (listenHttp)
                {
                    configure.Listen(
                        IPAddress.Parse(config.listenAddr),
                        (int)config.listenPort
                    );
                }
                else if (Socket.OSSupportsUnixDomainSockets)
                {
                    configure.ListenUnixSocket(
                        config.listenSocket,
                        (listenOptions) =>
                        {
                            if (!string.IsNullOrEmpty(listenOptions.SocketPath) && !OperatingSystem.IsWindows())
                            {
                                File.SetUnixFileMode(listenOptions.SocketPath, config.listenSocketPermission.ToUnixFileMode());
                            }
                        }
                    );
                }
                else
                {
                    throw new Exception("Cannot Bind to HTTP or Unix Domain Socket.");
                }
            }
        );
        if (config.isCheckPassed)
        {
            if (config.passwords is not null)
            {
                builder.Services.AddSingleton(config.passwords);
            }
            builder.Services.AddSingleton<IStatusManager, MemoryStatusManager>();
            builder.Services.AddSingleton<IUnixTimestampGenerator, UnixTimestampGenerator>();
            builder.Services.AddSingleton<ICertificatePasswordProvider, ConfigCertificatePasswordProvider>();
            builder.Services.AddHostedService<PrepareUsersService>();
            builder.Services.AddHostedService<ModulesCheckerService>();
            foreach (IModulesChecker checker in modulesCheckers)
            {
                builder.Services.AddSingleton<IModulesChecker>(checker);
            }
            Random random = new();
            IEnumerable<Type> graphAPICallerTypes = moduleTypesToAspNet.Where((t) => t.IsAssignableTo(typeof(IGraphAPICaller)));
            IEnumerable<Type> otherModuleTypesToAspNet = moduleTypesToAspNet.Where((t) => !graphAPICallerTypes.Contains(t));
            foreach (GraphUser user in config.users)
            {
                builder.Services.AddSingleton<GraphUser>(user);
                Type graphAPICallerType = random.GetItems(graphAPICallerTypes.ToArray(), 1)[0];
                builder.Services.AddKeyedSingleton(typeof(IGraphAPICaller), user, graphAPICallerType);
            }
            foreach (Type t in otherModuleTypesToAspNet)
            {
                builder.Services.AddSingleton(typeof(IAspNetModule), t);
            }
            IEnumerable<Type> apiFunctionsContainerTypes = Assembly.GetExecutingAssembly().GetTypes().GetNonAbstractClassesAssainableTo<IAPIFunctionsContainer>();
            foreach (Type t in apiFunctionsContainerTypes)
            {
                logger.LogDebug("Registering {0} as {1}", t.Name, nameof(IAPIFunctionsContainer));
                builder.Services.AddSingleton(typeof(IAPIFunctionsContainer), t);
            }
        }

        builder.Services.AddControllers().AddJsonOptions(
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
                    InvokeResult result = GenerateDummyResult(actionContext.HttpContext).Result;
                    return new JsonResult(result);
                };
            }
        );
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
        await app.RunAsync();
    }
);

CommandLineBuilder commandLineBuilder = new(rootCommand);
commandLineBuilder.UseHost(
    (host) =>
    {
        host.ConfigureServices(
        (services) =>
            {
                InjectModules(services, Assembly.GetExecutingAssembly());
                IEnumerable<Assembly> assemblies = GetPossibleModulesPaths().
                    Select(
                        (directory) =>
                        {
                            FileInfo[] files = directory.GetFiles(directory.Name + ".dll", SearchOption.TopDirectoryOnly);
                            if (files.Count() > 0)
                            {
                                ModuleLoadContext context = new(files[0]);
                                try
                                {
                                    Assembly assembly = context.LoadFromAssemblyName(
                                        new(Path.GetFileNameWithoutExtension(files[0].FullName))
                                    );
                                    return assembly;
                                }
                                catch { }
                            }
                            return null;
                        }
                    ).OfType<Assembly>();
                InjectModules(services, assemblies.ToArray());
            }
        );
        host.ConfigureLogging((
            logging) =>
            {
                logging.ClearProviders();
                logging.AddSimpleConsole(
                    (config) =>
                    {
                        config.SingleLine = true;
                        config.TimestampFormat = timeStampFormat;
                    }
                );
            }
        );
        host.UseEnvironment(Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Production");
    }
);
commandLineBuilder.UseDefaults();
return await commandLineBuilder.Build().InvokeAsync(args);

IEnumerable<Type> GetModules(Assembly assembly)
{
    return assembly.GetTypes().GetNonAbstractClassesAssainableTo<IModule>().Where(
        (t) => t.IsDefined(typeof(ModuleAttribute))
    );
}

IEnumerable<DirectoryInfo> GetPossibleModulesPaths()
{
    const string modulesBaseFolderName = "modules";
    const string modulesBaseFileName = "E5Renewer.Modules.*.dll";
    Dictionary<string, IEnumerable<DirectoryInfo>> results = new();
    DirectoryInfo assemblyDirectory = new(Path.Combine(Environment.CurrentDirectory, modulesBaseFolderName));
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

IServiceCollection InjectModules(IServiceCollection services, params Assembly[] assemblies)
{
    IEnumerable<Type> types = assemblies.
      Select(
        (assembly) => GetModules(assembly)
    ).SelectMany(
        (type) => type
    );
    List<Type> moduleTypesToAspNet = new();
    foreach (Type t in types)
    {
        if (t.IsAssignableTo(typeof(IModule)))
        {
            if (t.IsAssignableTo(typeof(IConfigParser)))
            {
                services.AddSingleton(typeof(IConfigParser), t);
            }
            else if (t.IsAssignableTo(typeof(IModulesChecker)))
            {
                services.AddSingleton(typeof(IModulesChecker), t);
                moduleTypesToAspNet.Add(t);
            }
            else if (t.IsAssignableTo(typeof(IAspNetModule)))
            {
                moduleTypesToAspNet.Add(t);
            }
        }
    }
    services.AddSingleton<List<Type>>(moduleTypesToAspNet);
    return services;
}

bool IsPortUsed(uint port)
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

async Task<InvokeResult> GenerateDummyResult(HttpContext context)
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
