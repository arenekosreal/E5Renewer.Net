using System.Reflection;

using E5Renewer.Controllers;
using E5Renewer.Controllers.V1;
using E5Renewer.Models.BackgroundServices;
using E5Renewer.Models.GraphAPIs;
using E5Renewer.Models.Modules;
using E5Renewer.Models.Secrets;
using E5Renewer.Models.Statistics;

namespace E5Renewer
{
    internal static class IServiceCollectionExtends
    {
        public static IServiceCollection AddUserClientProvider(this IServiceCollection services) =>
            services.AddSingleton<IUserClientProvider, SimpleUserClientProvider>();
        public static IServiceCollection AddDummyResultGenerator(this IServiceCollection services) =>
            services.AddTransient<IDummyResultGenerator, SimpleDummyResultGeneratorV1>();

        public static IServiceCollection AddHostedServices(this IServiceCollection services)
        {
            services.AddHostedService<PrepareUsersService>();
            return services;
        }

        public static IServiceCollection AddSecretProvider(this IServiceCollection services) =>
            services.AddSingleton<ISecretProvider, SimpleSecretProvider>();

        public static IServiceCollection AddStatusManager(this IServiceCollection services) =>
            services.AddSingleton<IStatusManager, MemoryStatusManager>();

        public static IServiceCollection AddTimeStampGenerator(this IServiceCollection services) =>
            services.AddTransient<IUnixTimestampGenerator, UnixTimestampGenerator>();

        public static IServiceCollection AddTokenOverride(this IServiceCollection services, string? token, FileInfo? tokenFile) =>
            services.AddSingleton<TokenOverride>(_ => new(token, tokenFile));

        public static IServiceCollection AddUserSecretFile(this IServiceCollection services, FileInfo userSecret) =>
            services.AddKeyedSingleton<FileInfo>(nameof(userSecret), userSecret);

        public static IServiceCollection AddModules(this IServiceCollection services, params Assembly[] assemblies)
        {
            Type[] knownModulesTypes = [
                typeof(IModulesChecker),
                typeof(IUserSecretLoader),
                typeof(IGraphAPICaller),
                typeof(IAPIFunction)
            ];
            foreach (Type t in assemblies.SelectMany((assembly) => assembly.IterE5RenewerModules()))
            {
                ServiceDescriptor service = ServiceDescriptor.Transient(
                    knownModulesTypes.FirstOrDefault(
                        (kt) => t.IsAssignableTo(kt),
                        typeof(IModule)),
                    t
                );
                if (!services.Contains(service))
                {
                    services.Add(service);
                }
            }
            return services;
        }
    }
}
