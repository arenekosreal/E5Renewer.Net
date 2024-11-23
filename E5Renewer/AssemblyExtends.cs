using System.Reflection;

using E5Renewer.Models.Modules;

namespace E5Renewer
{
    internal static class AssemblyExtends
    {
        public static IEnumerable<Type> IterE5RenewerModules(this Assembly assembly)
        {
            ModulesInAssemblyAttribute? attribute = assembly.GetCustomAttribute<ModulesInAssemblyAttribute>();
            if (attribute is not null)
            {
                foreach (Type t in attribute.types.GetNonAbstractClassesAssainableTo<IModule>())
                {
                    if (t.IsDefined(typeof(ModuleAttribute)))
                    {
                        yield return t;
                    }
                }
            }
        }
    }
}
