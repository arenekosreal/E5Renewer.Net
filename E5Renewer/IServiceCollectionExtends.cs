using System.Diagnostics.CodeAnalysis;
using System.Reflection;

using E5Renewer.Controllers;
using E5Renewer.Models.BackgroundServices;
using E5Renewer.Models.GraphAPIs;
using E5Renewer.Models.Modules;
using E5Renewer.Models.Secrets;
using E5Renewer.Models.Statistics;

namespace E5Renewer
{
    internal static class IServiceCollectionExtends
    {
        public static IServiceCollection AddDummyResultGenerator(this IServiceCollection services) =>
            services.AddTransient<IDummyResultGenerator, SimpleDummyResultGenerator>();

        [RequiresUnreferencedCode("Calls System.Reflection.Assembly.GetTypes()")]
        public static IServiceCollection AddAPIFunctionImplementations(this IServiceCollection services)
        {
            IEnumerable<Type> apiFunctionsTypes = Assembly.GetExecutingAssembly().GetTypes()
                .GetNonAbstractClassesAssainableTo<IAPIFunction>();
            foreach (Type t in apiFunctionsTypes)
            {
                services.AddSingleton(typeof(IAPIFunction), t);
            }
            return services;
        }

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

        [RequiresUnreferencedCode("Calls E5Renewer.AssemblyExtends.IterE5RenewerModules()")]
        public static IServiceCollection AddModules(this IServiceCollection services, params Assembly[] assemblies)
        {
            foreach (Assembly assembly in assemblies)
            {
                foreach (Type t in assembly.IterE5RenewerModules())
                {
                    if (t.IsAssignableTo(typeof(IModulesChecker)))
                    {
                        services.AddSingleton(typeof(IModulesChecker), t);
                    }
                    else if (t.IsAssignableTo(typeof(IUserSecretLoader)))
                    {
                        services.AddSingleton(typeof(IUserSecretLoader), t);
                    }
                    else if (t.IsAssignableTo(typeof(IGraphAPICaller)))
                    {
                        services.AddSingleton(typeof(IGraphAPICaller), t);
                    }
                    else if (t.IsAssignableTo(typeof(IModule)))
                    {
                        services.AddSingleton(typeof(IModule), t);
                    }
                }
            }
            return services;
        }
    }
}
