using System.Reflection;

using E5Renewer;
using E5Renewer.Controllers;
using E5Renewer.Controllers.V1;
using E5Renewer.Models.Modules;

using Microsoft.AspNetCore.Mvc;

const UnixFileMode defaultListenUnixSocketPermission =
    UnixFileMode.UserRead | UnixFileMode.UserWrite |
    UnixFileMode.GroupRead | UnixFileMode.GroupWrite |
    UnixFileMode.OtherRead | UnixFileMode.OtherWrite;

// Variables from Configuration
bool systemd;
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
    {"--listen-unix-socket-permission", nameof(listenUnixSocketPermission)}
};

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddCommandLine(args, commandLineSwitchMap);

systemd = args.ContainsFlag(nameof(systemd)) || builder.Configuration.GetValue<bool>(nameof(systemd));
listenUnixSocketPermission = builder.Configuration.GetValue<UnixFileMode>(
    nameof(listenUnixSocketPermission), defaultListenUnixSocketPermission);
userSecret = builder.Configuration.GetValue<string>(nameof(userSecret))?.AsFileInfo();
tokenFile = builder.Configuration.GetValue<string>(nameof(tokenFile))?.AsFileInfo();
token = builder.Configuration.GetValue<string>(nameof(token));

builder.Logging.AddConsole(systemd, builder.Environment.IsDevelopment() ? LogLevel.Debug : LogLevel.Information);

IEnumerable<Assembly> assembliesInAttribute = Assembly.GetExecutingAssembly()
    .GetCustomAttributes<AssemblyContainsModuleAttribute>()
    .Select((attribute) => attribute.assembly);
IEnumerable<Assembly> assembliesInFilesystem = GetPossibleModulesPaths()
    .Select(
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
    )
    .OfType<Assembly>();

builder.Services
    .AddModules(assembliesInAttribute.Concat(assembliesInFilesystem).ToArray())
    .AddUserSecretFile(userSecret.EnsureNotNull(nameof(userSecret)))
    .AddTokenOverride(token, tokenFile)
    .AddDummyResultGenerator()
    .AddSecretProvider()
    .AddStatusManager()
    .AddTimeStampGenerator()
    .AddUserClientProvider()
    .AddHostedServices()
    .AddControllers()
    .AddJsonOptions(
    (options) =>
        options.JsonSerializerOptions.TypeInfoResolverChain.Add(JsonAPIV1ResponseJsonSerializerContext.Default)
    )
    .ConfigureApiBehaviorOptions(
        (options) =>
            options.InvalidModelStateResponseFactory =
                (actionContext) =>
                {
                    IJsonResponse result = actionContext.HttpContext.RequestServices.GetRequiredService<IDummyResultGenerator>()
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
                IJsonResponse result = await context.RequestServices.GetRequiredService<IDummyResultGenerator>()
                    .GenerateDummyResultAsync(context);
                await context.Response.WriteAsJsonAsync(result, JsonAPIV1ResponseJsonSerializerContext.Default.JsonAPIV1Response);
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
const string unixDomainSocketUrlPrefixHttp = "http://unix:";
const string unixDomainSocketUrlPrefixHttps = "https://unix:";
IEnumerable<string> filteredUrls =
    app.Urls
        .TakeWhile((url) =>
                url.StartsWith(unixDomainSocketUrlPrefixHttp)
            || url.StartsWith(unixDomainSocketUrlPrefixHttps))
        .Select((url) =>
                url.StartsWith(unixDomainSocketUrlPrefixHttp)
            ? url.Substring(unixDomainSocketUrlPrefixHttp.Length)
            : url.Substring(unixDomainSocketUrlPrefixHttps.Length))
        .TakeWhile((url) =>
                !string.IsNullOrEmpty(url)
            && !string.IsNullOrWhiteSpace(url));
foreach (string url in filteredUrls)
{
    new FileInfo(url).SetUnixFileMode(listenUnixSocketPermission);
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
