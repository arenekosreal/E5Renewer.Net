using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace E5Renewer.Models.SourceGenerators.ModulesInAssemblyGenerator
{
    /// <summary>Scan <c>E5Renewer.Models.Modules.ModuleAttribute</c>
    /// and generate <c>E5Renewer.Models.Modules.ModulesInAssemblyAttribute</c>
    /// </summary>
    [Generator(LanguageNames.CSharp)]
    public class ModulesInAssemblyGenerator : IIncrementalGenerator
    {
        private const string moduleAttributeFqdn = "E5Renewer.Models.Modules.ModuleAttribute";
        private const string iModuleInterfaceFqdn = "E5Renewer.Models.Modules.IModule";
        private const string iModuleInterfaceName = "IModule";
        private const string modulesInAssemblyAttributeFqdn = "E5Renewer.Models.Modules.ModulesInAssemblyAttribute";

        /// <inheritdoc/>
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            IncrementalValueProvider<ImmutableArray<GeneratorAttributeSyntaxContext>> moduleClassDefines =
                context.SyntaxProvider.ForAttributeWithMetadataName(
                    moduleAttributeFqdn,
                    (node, _) => node.IsKind(SyntaxKind.ClassDeclaration),
                    (syntaxContext, _) => syntaxContext
                ).Collect();
            context.RegisterSourceOutput(moduleClassDefines, this.Generate);
        }

        private void Generate(SourceProductionContext context, ImmutableArray<GeneratorAttributeSyntaxContext> syntaxContexts)
        {
            List<string> lines = new List<string>();

            foreach (GeneratorAttributeSyntaxContext syntaxContext in syntaxContexts)
            {
                if (syntaxContext.TargetSymbol is INamedTypeSymbol symbol
                // TODO: Use FQDN instead.
                && symbol.AllInterfaces.Any((i) => i.Name == iModuleInterfaceName)
                )
                {
                    lines.Add($"[assembly: {modulesInAssemblyAttributeFqdn}(typeof({symbol.ToDisplayString()}))]");
                }
            }

            context.AddSource(
                $"ModulesInAssembly.g",
                string.Join("\n", lines)
            );
        }
    }
}
