using System.Reflection;

namespace E5Renewer.Modules
{
    public static class ModulesLoader
    {
        private static readonly ILogger logger = LoggerFactory.Create(
            (build) => build.AddSimpleConsole(
                (options) =>
                {
                    options.SingleLine = true;
                    options.TimestampFormat = E5Renewer.Constraints.loggingTimeFormat;
                }
            ).SetMinimumLevel(E5Renewer.Constraints.loggingLevel)
        ).CreateLogger(typeof(ModulesLoader));
        private static readonly string binaryBaseDir = AppDomain.CurrentDomain.BaseDirectory;
        private static readonly List<IModule> modules = new();
        private static List<T> GetModulesInAssembly<T>(Assembly assembly)
            where T : class, IModule
        {
            const string moduleNameSpace = "E5Renewer.Modules.";
            List<T> modules = new();
            try
            {
                foreach (Type t in assembly.GetTypes().Where(
                    (Type t) => typeof(T).IsAssignableFrom(t) && (t.Namespace?.StartsWith(moduleNameSpace) ?? false) && t.IsDefined(typeof(ModuleAttribute))
                ))
                {
                    if (t.FullName != null)
                    {
                        object? obj = assembly.CreateInstance(t.FullName);
                        if (obj != null)
                        {
                            T? module = obj as T;
                            if (module != null)
                            {
                                modules.Add(module);
                                logger.LogInformation("Loaded module {0} by {1}", module.name, module.author);
                                if (module.IsDeprecated)
                                {
                                    logger.LogWarning("You are using a deprecated module!");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                logger.LogInformation("Failed to load module because {0}", exc.Message);
            }
            return modules;
        }
        public static void LoadModules()
        {
            modules.AddRange(GetModulesInAssembly<IModule>(Assembly.GetExecutingAssembly()));

            DirectoryInfo directoryInfo = new(binaryBaseDir);
            foreach (FileInfo fileInfo in directoryInfo.GetFiles("E5Renewer.Modules.*.dll", SearchOption.TopDirectoryOnly))
            {
                modules.AddRange(GetModulesInAssembly<IModule>(Assembly.Load(Path.GetFileNameWithoutExtension(fileInfo.FullName))));
            }
        }
        public static List<T> GetRegisteredModules<T>()
            where T : class, IModule
        {
            List<T> results = new();
            modules.ForEach(
                delegate (IModule module)
                {
                    T? c = module as T;
                    if (c != null)
                    {
                        results.Add(c);
                    }
                }
            );
            return results;
        }
    }
}
