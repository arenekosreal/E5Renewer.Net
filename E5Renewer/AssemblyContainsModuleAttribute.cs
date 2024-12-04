using System.Reflection;

namespace E5Renewer;

[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
internal class AssemblyContainsModuleAttribute : Attribute
{
    private string assemblyName { get; }

    public Assembly assembly { get => AppDomain.CurrentDomain.Load(this.assemblyName); }

    public AssemblyContainsModuleAttribute(string assemblyName) => this.assemblyName = assemblyName;

}
