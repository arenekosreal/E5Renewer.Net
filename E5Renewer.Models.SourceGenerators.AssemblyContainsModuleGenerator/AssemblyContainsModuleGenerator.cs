using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

using Microsoft.CodeAnalysis;

namespace E5Renewer.Models.SourceGenerators.AssemblyContainsModuleGenerator
{
    /// <summary>Scan referenced assembly and
    /// add them into <c>AssemblyContainsModuleAttribute</c>
    /// </summary>
    [Generator(LanguageNames.CSharp)]
    public class AssemblyContainsModuleGenerator : IIncrementalGenerator
    {
        private const string modulesInAssemblyFqdn = "E5Renewer.Models.Modules.ModulesInAssemblyAttribute";
        private const string assemblyContainsModuleAttributeFqdn = "E5Renewer.AssemblyContainsModuleAttribute";

        /// <inheritdoc/>
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            IncrementalValueProvider<ImmutableArray<IAssemblySymbol>> assemblySymbols =
                context.CompilationProvider.Select((c, _) => c.SourceModule.ReferencedAssemblySymbols);
            context.RegisterSourceOutput(assemblySymbols, this.Generate);
        }

        private void Generate(SourceProductionContext context, ImmutableArray<IAssemblySymbol> assemblySymbols)
        {
            List<string> stringLines = new List<string>();
            foreach (IAssemblySymbol assemblySymbol in assemblySymbols)
            {
                if (assemblySymbol.GetAttributes().Any((item) => item.AttributeClass.ToDisplayString() == modulesInAssemblyFqdn))
                {
                    stringLines.Add($"[assembly: {assemblyContainsModuleAttributeFqdn}(\"{assemblySymbol.ToDisplayString()}\")]");
                }
            }
            context.AddSource("AssemblyContainsModule.g", string.Join("\n", stringLines));
        }
    }
}
