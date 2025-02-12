using System.Reflection;

using CaseConverter;

using E5Renewer;
using E5Renewer.Controllers;
using E5Renewer.Controllers.V1;
using E5Renewer.Models.Modules;

using Microsoft.AspNetCore.Mvc;

const UnixFileMode defaultListenUnixSocketPermission =
    UnixFileMode.UserRead | UnixFileMode.UserWrite |
    UnixFileMode.GroupRead | UnixFileMode.GroupWrite |
    UnixFileMode.OtherRead | UnixFileMode.OtherWrite;

const string modulesInFilesystemBaseDirectoryName = "modules";
const string commandLineArgumentPrefixLong = "--";

// Variables from Configuration
bool systemd;
UnixFileMode listenUnixSocketPermission;
FileInfo? userSecret;
FileInfo? tokenFile;
string? token;

Dictionary<string, string> commandLineSwitchMap = new()
{
    {$"{commandLineArgumentPrefixLong}{nameof(systemd).ToKebabCase()}", nameof(systemd).ToTitleCase()},
    {$"{commandLineArgumentPrefixLong}{nameof(userSecret).ToKebabCase()}", nameof(userSecret).ToTitleCase()},
    {$"{commandLineArgumentPrefixLong}{nameof(token).ToKebabCase()}", nameof(token).ToTitleCase()},
    {$"{commandLineArgumentPrefixLong}{nameof(tokenFile).ToKebabCase()}", nameof(tokenFile).ToTitleCase()},
    {$"{commandLineArgumentPrefixLong}{nameof(listenUnixSocketPermission).ToKebabCase()}", nameof(listenUnixSocketPermission).ToTitleCase()}
};

WebApplicationBuilder builder = WebApplication.CreateBuilder();
builder.Configuration.AddCommandLine(args, commandLineSwitchMap);

systemd = args.ContainsFlag(nameof(systemd).ToKebabCase(), commandLineArgumentPrefixLong) ||
            builder.Configuration.GetValue<bool>(nameof(systemd).ToTitleCase());
listenUnixSocketPermission = builder.Configuration.GetValue<UnixFileMode>(
    nameof(listenUnixSocketPermission).ToTitleCase(), defaultListenUnixSocketPermission);
userSecret = builder.Configuration.GetValue<string>(nameof(userSecret).ToTitleCase())?.AsFileInfo();
tokenFile = builder.Configuration.GetValue<string>(nameof(tokenFile).ToTitleCase())?.AsFileInfo();
token = builder.Configuration.GetValue<string>(nameof(token).ToTitleCase());

IEnumerable<Assembly> assembliesToLoad = Assembly.GetExecutingAssembly()
    .GetCustomAttributes<AssemblyContainsModuleAttribute>()
    .Select((attribute) => attribute.assembly);

#if ! E5RENEWER_AOT
DirectoryInfo modulesInFilesystemBaseDirectory = new(Path.Combine(AppContext.BaseDirectory, modulesInFilesystemBaseDirectoryName));
if (modulesInFilesystemBaseDirectory.Exists)
{
    IEnumerable<Assembly> assembliesInFilesystem = modulesInFilesystemBaseDirectory.GetDirectories()
        .Select(
        (directory) =>
            {
                string searchFileName = directory.Name + ".dll";
                if (!searchFileName.ContainsAny(["*", "?"]))
                {
                    FileInfo[] files = directory.GetFiles(searchFileName, SearchOption.TopDirectoryOnly);
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
                }
                return null;
            }
        )
        .OfType<Assembly>();
    assembliesToLoad = assembliesToLoad.Concat(assembliesInFilesystem);
}
#endif // ! E5RENEWER_AOT

builder.Logging.AddConsole(systemd);

builder.Services
    .AddModules(assembliesToLoad)
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
        .TakeWhile((url) => !string.IsNullOrWhiteSpace(url));
foreach (string url in filteredUrls)
{
    new FileInfo(url).SetUnixFileMode(listenUnixSocketPermission);
}
await app.WaitForShutdownAsync();
