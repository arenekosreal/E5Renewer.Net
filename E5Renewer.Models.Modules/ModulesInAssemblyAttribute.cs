namespace E5Renewer.Models.Modules;

/// <summary>The attribute for marking assembly with module types.</summary>
[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
public class ModulesInAssemblyAttribute : Attribute
{
    /// <value>Module type array.</value>
    public readonly Type[] types;

    /// <summary>Initialize <see cref="ModulesInAssemblyAttribute"/> instance with arguments given.</summary>
    public ModulesInAssemblyAttribute(params Type[] types) => this.types = types;
}

