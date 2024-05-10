using System.CommandLine;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using E5Renewer.Config;
using E5Renewer.Modules;
using E5Renewer.Exceptions;
using E5Renewer.Processor;
using E5Renewer.Statistics;

Option<FileInfo?> configFile = new(
    "--config",
    "The config file to use."
);
RootCommand rootCommand = new(
    "Renew Microsoft E5 subscription by calling msgraph APIs."
);

rootCommand.AddOption(configFile);
rootCommand.SetHandler(
    async delegate (FileInfo? fileInfo)
    {
        ModulesLoader.LoadModules();
        RuntimeConfig config = fileInfo != null ? await ConfigParser.ParseConfig(fileInfo) : new RuntimeConfig();
        if (config.check)
        {
            E5Renewer.Constraints.loggingLevel = config.debug ? LogLevel.Debug : LogLevel.Information;
            await StartWebApplication(config);
        }
        else
        {
            throw new InvalidConfigException("Invalid config.");
        }
    },
    configFile
);
return await rootCommand.InvokeAsync(args);



async Task StartWebApplication(RuntimeConfig config)
{
    WebApplicationBuilder builder = WebApplication.CreateBuilder();
    builder.Logging.ClearProviders();
    builder.Logging.AddSimpleConsole(
        (options) =>
        {
            options.SingleLine = true;
            options.TimestampFormat = E5Renewer.Constraints.loggingTimeFormat;
        }
    ).SetMinimumLevel(E5Renewer.Constraints.loggingLevel);
    builder.WebHost.ConfigureKestrel(
        delegate (KestrelServerOptions serverOptions)
        {
            const uint maxPort = 65535;
            bool listenHttp =
                !string.IsNullOrEmpty(config.listenAddr) &&
                config.listenPort > 0 &&
                config.listenPort <= maxPort;
            if (listenHttp)
            {
                serverOptions.Listen(
                    IPAddress.Parse(config.listenAddr),
                    (int)config.listenPort
                );
            }
            else if (Socket.OSSupportsUnixDomainSockets)
            {
                serverOptions.ListenUnixSocket(
                    config.listenSocket,
                    delegate (ListenOptions listenOptions)
                    {
                        if (listenOptions.SocketPath != null && !OperatingSystem.IsWindows())
                        {
                            File.SetUnixFileMode(
                                listenOptions.SocketPath,
                                Helper.ToUnixFileMode(config.listenSocketPermission)
                            );
                        }
                    }
                );
            }
            else
            {
                throw new RuntimeException("Cannot Bind to HTTP or Unix Domain Socket.");
            }
        }
    );
    builder.Services.AddHostedService<GraphAPIProcessor>();
    builder.Services.AddSingleton<List<GraphUser>>(config.users);
    builder.Services.AddControllers(
        ).AddJsonOptions(
            (options) =>
            {
                options.JsonSerializerOptions.WriteIndented = true;
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
            }
        ).ConfigureApiBehaviorOptions(
            (options) =>
                options.InvalidModelStateResponseFactory = (context) => ExceptionHandlers.GenerateExceptionResult(context).Result
        );
    WebApplication app = builder.Build();
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


