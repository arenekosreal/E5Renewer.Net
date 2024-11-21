using System.Reflection;

using E5Renewer.Models.Modules;

namespace E5Renewer
{
    internal static class AssemblyExtends
    {
        public static IEnumerable<Type> IterE5RenewerModules(this Assembly assembly)
        {
            return assembly.GetTypes().GetNonAbstractClassesAssainableTo<IModule>().Where(
                (t) => t.IsDefined(typeof(ModuleAttribute))
            );
        }
    }
}
