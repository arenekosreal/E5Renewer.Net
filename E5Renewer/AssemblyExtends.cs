using System.Reflection;

using E5Renewer.Models.Modules;

namespace E5Renewer
{
    internal static class AssemblyExtends
    {
        public static IEnumerable<Type> IterE5RenewerModules(this Assembly assembly)
        {
            return assembly.GetCustomAttributes<ModulesInAssemblyAttribute>()
                .SelectMany((t) => t.types.GetNonAbstractClassesAssainableTo<IModule>())
                .Where((t) => t.IsDefined(typeof(ModuleAttribute)));
        }
    }
}
