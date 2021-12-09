using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace NotifyPropertyChangedGenerator
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
                    GenerateRequestModel(context, typeSymbol);
                }
            }
    }

        private void GenerateRequestModel(GeneratorExecutionContext context, ITypeSymbol typeSymbol)
        {
            var source = GenerateRequestClass(typeSymbol);
            context.AddSource($"{typeSymbol.Name}Requests.cs", source);
        }

        private string GenerateRequestClass(ITypeSymbol typeSymbol)
    {
      return $@"
using System;

namespace {typeSymbol.ContainingNamespace}
{{
  public class {typeSymbol.Name}Request
  {{
    {GenerateProperties(typeSymbol)}
  }}
}}";
    }

    private static string GenerateProperties(ITypeSymbol typeSymbol)
    {
      var sb = new StringBuilder();

      foreach (var propertySymbol in typeSymbol.GetMembers().OfType<IPropertySymbol>())
      {

        var propertyName = propertySymbol.Name;
        sb.AppendLine($@"
    public {propertySymbol.Type} {propertyName} {{ get; set; }}
  ");
      }

      return sb.ToString();
    }
  }
}
