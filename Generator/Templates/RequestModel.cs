using Microsoft.CodeAnalysis;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CatastrofizerGenerator.Templates
{
    public static class RequestModel
    {
        public static string GenerateSource(ITypeSymbol typeSymbol, IEnumerable<IPropertySymbol> properties)
        {
            using var writer = new StringWriter();
            using var indentWriter = new IndentedTextWriter(writer, new string(' ', 4));
            var projectBase = typeSymbol.ContainingNamespace.ToString().Split('.')[0];
            WriteFileContents(typeSymbol, properties, indentWriter, projectBase);
            return writer.ToString();
        }

        private static void WriteFileContents(ITypeSymbol typeSymbol, IEnumerable<IPropertySymbol> properties, IndentedTextWriter indentWriter, string projectBase)
        {
            indentWriter.WriteLine("using System;");
            indentWriter.WriteLine($"using {projectBase};");
            indentWriter.WriteLine($"namespace {projectBase}.Models");
            indentWriter.OpenCodeBlock();
            WriteClass(typeSymbol, properties, indentWriter);
            GenerateMapFunction(typeSymbol.Name, properties, indentWriter);
            indentWriter.CloseCodeBlock();
        }

        private static void WriteClass(ITypeSymbol typeSymbol, IEnumerable<IPropertySymbol> properties, IndentedTextWriter indentWriter)
        {
            indentWriter.WriteLine($"public class {typeSymbol.Name}Request");
            indentWriter.OpenCodeBlock();
            RequestProperties.Generate(properties, indentWriter);
            indentWriter.CloseCodeBlock();
        }

        private static void GenerateMapFunction(string name, IEnumerable<IPropertySymbol> propertySymbols, IndentedTextWriter indentWriter)
        {
            indentWriter.WriteLine($"public static class {name}RequestExtensions");
            indentWriter.OpenCodeBlock();
            indentWriter.WriteLine($" public static {name} ToDomain(this {name}Request dto) =>");
            indentWriter.Indent++;
            var parameters = propertySymbols.Select(p => $"{p.Name}: dto.{p.Name}");
            indentWriter.WriteLine($"new {name}({string.Join(", ", parameters)});");
            indentWriter.Indent--;
            indentWriter.CloseCodeBlock();
        }
    }
}
