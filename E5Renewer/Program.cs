using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text.Json;

using E5Renewer;
using E5Renewer.Controllers;
using E5Renewer.Models.Modules;

using Microsoft.AspNetCore.Mvc;

const UnixFileMode defaultListenUnixSocketPermission =
    UnixFileMode.UserRead | UnixFileMode.UserWrite |
    UnixFileMode.GroupRead | UnixFileMode.GroupWrite |
    UnixFileMode.OtherRead | UnixFileMode.OtherWrite;
bool setSocketPermission = false;

// Variables from Configuration
bool systemd;
IPEndPoint? listenTcpSocket;
string? listenUnixSocketPath;
UnixFileMode listenUnixSocketPermission;
FileInfo? userSecret;
FileInfo? tokenFile;
string? token;

Dictionary<string, string> commandLineSwitchMap = new()
{
    {"--systemd", nameof(systemd)},
    {"--user-secret", nameof(userSecret)},
    {"--token", nameof(token)},
    {"--token-file", nameof(tokenFile)},
    {"--listen-tcp-socket", nameof(listenTcpSocket)},
    {"--listen-unix-socket-path", nameof(listenUnixSocketPath)},
    {"--listen-unix-socket-permission", nameof(listenUnixSocketPermission)}
};

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddCommandLine(args, commandLineSwitchMap);

systemd = args.ContainsFlag(nameof(systemd)) || builder.Configuration.GetValue<bool>(nameof(systemd));
listenTcpSocket = builder.Configuration.GetValue<string>(nameof(listenTcpSocket))?.AsIPEndPoint();
listenUnixSocketPath = builder.Configuration.GetValue<string>(nameof(listenUnixSocketPath));
listenUnixSocketPermission = builder.Configuration.GetValue<UnixFileMode>(
    nameof(listenUnixSocketPermission), defaultListenUnixSocketPermission);
userSecret = builder.Configuration.GetValue<string>(nameof(userSecret))?.AsFileInfo();
tokenFile = builder.Configuration.GetValue<string>(nameof(tokenFile))?.AsFileInfo();
token = builder.Configuration.GetValue<string>(nameof(token));

builder.Logging.AddConsole(systemd, builder.Environment.IsDevelopment() ? LogLevel.Debug : LogLevel.Information);

builder.WebHost.ConfigureKestrel(
(kestrelServerOptions) =>
    {
        if (listenTcpSocket is not null)
        {
            kestrelServerOptions.Listen(listenTcpSocket);
        }

        if (listenUnixSocketPath is not null && Socket.OSSupportsUnixDomainSockets)
        {
            kestrelServerOptions.ListenUnixSocket(listenUnixSocketPath);
            setSocketPermission = true;
        }
    }
);

builder.Services.AddModules(Assembly.GetExecutingAssembly());
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
builder.Services.AddModules(assemblies.ToArray());

if (userSecret is not null)
{
    builder.Services.AddUserSecretFile(userSecret);
}
else
{
    throw new NullReferenceException(nameof(userSecret));
}

builder.Services
    .AddTokenOverride(token, tokenFile)
    .AddDummyResultGenerator()
    .AddSecretProvider()
    .AddStatusManager()
    .AddTimeStampGenerator()
    .AddAPIFunctionImplementations()
    .AddHostedServices()
    .AddControllers()
    .AddJsonOptions(
    (options) =>
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    )
    .ConfigureApiBehaviorOptions(
        (options) =>
            options.InvalidModelStateResponseFactory =
                (actionContext) =>
                {
                    InvokeResult result = actionContext.HttpContext.RequestServices.GetRequiredService<IDummyResultGenerator>()
                            .GenerateDummyResult(actionContext.HttpContext);
                    return new JsonResult(result);
                }
    );


WebApplication app = builder.Build();

app.UseModulesCheckers();

app.UseExceptionHandler(
    (exceptionHandlerApp) =>
        exceptionHandlerApp.Run(
            async (context) =>
            {
                InvokeResult result = await context.RequestServices.GetRequiredService<IDummyResultGenerator>()
                    .GenerateDummyResultAsync(context);
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
app.UseAuthTokenAuthentication();
app.Logger.LogDebug("Mapping controllers");
app.MapControllers();
await app.StartAsync();
if (setSocketPermission && !OperatingSystem.IsWindows() && listenUnixSocketPath is not null)
{
    File.SetUnixFileMode(listenUnixSocketPath, listenUnixSocketPermission);
}
await app.WaitForShutdownAsync();

static IEnumerable<DirectoryInfo> GetPossibleModulesPaths()
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
