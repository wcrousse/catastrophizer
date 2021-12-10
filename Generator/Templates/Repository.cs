using Microsoft.CodeAnalysis;
using System.CodeDom.Compiler;
using System.IO;

namespace CatastrofizerGenerator.Templates
{
    internal class Repository
    {
        public static string GenerateSource(ITypeSymbol typeSymbol)
        {
            using var writer = new StringWriter();
            using var indentWriter = new IndentedTextWriter(writer, new string(' ', 4));
            var projectBase = typeSymbol.ContainingNamespace.ToString().Split('.')[0];
            WriteSourceFileContents(typeSymbol, indentWriter, projectBase);
            return writer.ToString();

        }
        private static void WriteSourceFileContents(ITypeSymbol typeSymbol, IndentedTextWriter indentWriter, string projectBase)
        {
            WriteDependencyList(indentWriter, projectBase);
            WriteNameSpace(typeSymbol, indentWriter, projectBase);
        }


        private static void WriteDependencyList(IndentedTextWriter indentWriter, string projectBase)
        {
            indentWriter.WriteLine("using System;");
            indentWriter.WriteLine("using System.Collections.Generic;");
            indentWriter.WriteLine("using System.Data;");
            indentWriter.WriteLine("using System.Linq;");
            indentWriter.WriteLine("using System.Threading.Tasks;");
            indentWriter.WriteLine("using Dapper;");
            indentWriter.WriteLine($"using {projectBase}.Models;");
        }
        private static void WriteNameSpace(ITypeSymbol typeSymbol, IndentedTextWriter indentWriter, string projectBase)
        {
            indentWriter.WriteLine($"namespace {projectBase}.Repositories");
            indentWriter.OpenCodeBlock();
            indentWriter.WriteLineNoTabs("");
            WriteClass(typeSymbol, indentWriter);
            indentWriter.CloseCodeBlock();
        }

        private static void WriteClass(ITypeSymbol typeSymbol, IndentedTextWriter indentWriter)
        {
            var resourceName = typeSymbol.Name;
            var resourceVariableName = typeSymbol.Name.ToCamelCase();
            var repositoryName = $"_{resourceVariableName}Repository";
            WriteClassDeclaration(indentWriter, resourceName);
            indentWriter.OpenCodeBlock();
            indentWriter.WriteLineNoTabs("");

            WriteCreateMethod(indentWriter, resourceName, resourceVariableName, repositoryName);
            WriteGetMethod(indentWriter, resourceName, resourceVariableName, repositoryName);

            indentWriter.CloseCodeBlock();
        }

        private static void WriteGetMethod(IndentedTextWriter indentWriter, string resourceName, string resourceVariableName, string repositoryName)
        {
            indentWriter.WriteLine($"public async Task<{resourceName}?>Get(int {resourceVariableName}Id)");
            indentWriter.OpenCodeBlock();
            indentWriter.WriteLine($"return null;");
            indentWriter.CloseCodeBlock();
        }

        private static void WriteCreateMethod(IndentedTextWriter indentWriter, string resourceName, string resourceVariableName, string repositoryName)
        {
            indentWriter.WriteLine($"public async Task<int>Create({resourceName} resourceVariableName)");
            indentWriter.OpenCodeBlock();
            indentWriter.WriteLine($"return {12};");
            indentWriter.CloseCodeBlock();
        }

        private static void WriteClassDeclaration(IndentedTextWriter indentWriter, string resourceName)
        {
            indentWriter.WriteLine($"public partial class {resourceName}Repository");
        }
    }
}
