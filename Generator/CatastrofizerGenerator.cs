using System.Collections.Immutable;
using System.Linq;
using CatastrofizerGenerator.Templates;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Catastrofizer
{
    [Generator]
    public class CatastrofizerGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
        }

        public void Execute(GeneratorExecutionContext context)
        {
            // uncomment to debug the actual build of the target project
            //  Debugger.Launch();
            var compilation = context.Compilation;
            var catastrofizableInterface = compilation.GetTypeByMetadataName("CatastrofizerGenerator.ICatastrofizable");

            foreach (var syntaxTree in compilation.SyntaxTrees)
            {
                var semanticModel = compilation.GetSemanticModel(syntaxTree);
                var immutableHashSet = syntaxTree.GetRoot()
                  .DescendantNodesAndSelf()
                  .OfType<ClassDeclarationSyntax>()
                  .Select(x => semanticModel.GetDeclaredSymbol(x))
                  .OfType<ITypeSymbol>()
                  .Where(x => x.Interfaces.Contains(catastrofizableInterface, SymbolEqualityComparer.Default))
                  .ToImmutableHashSet();

                foreach (var typeSymbol in immutableHashSet)
                {
                    AddConstructor(context, typeSymbol);
                    GenerateRequestModel(context, typeSymbol);
                    GenerateRepository(context, typeSymbol);
                    GenerateController(context, typeSymbol);
                }
            }
        }

        private void GenerateRepository(GeneratorExecutionContext context, ITypeSymbol typeSymbol)
        {
            var requestModelSource = Repository.GenerateSource(typeSymbol);
            context.AddSource($"{typeSymbol.Name}Repository.generated.cs", requestModelSource);
        }

        private void AddConstructor(GeneratorExecutionContext context, ITypeSymbol typeSymbol)
        {
            var requestModelSource = DomainModel.GenerateSource(typeSymbol);
            context.AddSource($"{typeSymbol.Name}.Generated.cs", requestModelSource);
        }

        private void GenerateRequestModel(GeneratorExecutionContext context, ITypeSymbol typeSymbol)
        {
            var propertySymbols = typeSymbol.GetMembers().OfType<IPropertySymbol>();
            var requestModelSource = RequestModel.GenerateSource(typeSymbol, propertySymbols);
            context.AddSource($"{typeSymbol.Name}Requests.cs", requestModelSource);
        }

        private void GenerateController(GeneratorExecutionContext context, ITypeSymbol typeSymbol)
        {
            var requestModelSource = Controller.GenerateSource(typeSymbol);
            context.AddSource($"{typeSymbol.Name}Controller.generated.cs", requestModelSource);
        }
    }
}
