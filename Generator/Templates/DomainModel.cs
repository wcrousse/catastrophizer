using Microsoft.CodeAnalysis;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CatastrofizerGenerator.Templates
{
    internal class DomainModel
    {
        public static string GenerateSource(ITypeSymbol typeSymbol)
        {
            using var writer = new StringWriter();
            using var indentWriter = new IndentedTextWriter(writer, new string(' ', 4));

            var properties = typeSymbol.GetMembers().OfType<IPropertySymbol>();

            WriteSourceFileContents(typeSymbol, indentWriter, properties);
            return writer.ToString();
        }

        private static void WriteSourceFileContents(ITypeSymbol typeSymbol, IndentedTextWriter indentWriter, IEnumerable<IPropertySymbol> properties)
        {
            indentWriter.WriteLine($" namespace {typeSymbol.ContainingNamespace}");
            indentWriter.OpenCodeBlock();
            WriteClass(typeSymbol, indentWriter, properties);
            indentWriter.CloseCodeBlock();
        }

        private static void WriteClass(ITypeSymbol typeSymbol, IndentedTextWriter indentWriter, IEnumerable<IPropertySymbol> properties)
        {
            indentWriter.WriteLine($"partial class {typeSymbol.Name}");
            indentWriter.OpenCodeBlock();
            WriteConstructor(typeSymbol, indentWriter, properties);
            indentWriter.CloseCodeBlock();
        }

        private static void WriteConstructor(ITypeSymbol typeSymbol, IndentedTextWriter indentWriter, IEnumerable<IPropertySymbol> properties)
        {
            var parameters = properties.Select(p => $"{p.Type.ToDisplayString()} {p.Name}");
            indentWriter.WriteLine($"public {typeSymbol.Name}( {string.Join(", ", parameters)})");
            WriteConstructorBody(indentWriter, properties);
        }

        private static void WriteConstructorBody(IndentedTextWriter indentWriter, IEnumerable<IPropertySymbol> properties)
        {
            indentWriter.OpenCodeBlock();
            foreach (var property in properties)
            {
                indentWriter.WriteLine($"this.{property.Name} = {property.Name};");
            }
            indentWriter.CloseCodeBlock();
        }
    }
}
