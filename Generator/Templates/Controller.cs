using Microsoft.CodeAnalysis;
using System.CodeDom.Compiler;
using System.IO;

namespace CatastrofizerGenerator.Templates
{
    internal class Controller
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

        private static void WriteNameSpace(ITypeSymbol typeSymbol, IndentedTextWriter indentWriter, string projectBase)
        {
            indentWriter.WriteLine($"namespace {projectBase}.Controllers");
            indentWriter.OpenCodeBlock();
            indentWriter.WriteLineNoTabs("");
            WriteClass(typeSymbol, indentWriter);
            indentWriter.CloseCodeBlock();
        }

        private static void WriteDependencyList(IndentedTextWriter indentWriter, string projectBase)
        {
            indentWriter.WriteLine("using System;");
            indentWriter.WriteLine($"using {projectBase}.Repositories;");
            indentWriter.WriteLine($"using {projectBase}.Models;");
            indentWriter.WriteLine($"using Microsoft.AspNetCore.Http;");
            indentWriter.WriteLine($"using Microsoft.AspNetCore.Mvc;");
            indentWriter.WriteLine($"using System.Collections.Generic;");
            indentWriter.WriteLine($"using System.Threading;");
            indentWriter.WriteLine($"using System.Threading.Tasks;");
        }

        private static void WriteClass(ITypeSymbol typeSymbol, IndentedTextWriter indentWriter)
        {
            var resourceName = typeSymbol.Name;
            var resourceVariableName = typeSymbol.Name.ToCamelCase();
            var repositoryType = $"{typeSymbol.Name}Repository";
            var repositoryName = $"_{resourceVariableName}Repository";
            WriteClassDeclaration(indentWriter, resourceName);
            indentWriter.OpenCodeBlock();
            indentWriter.WriteLine($"private readonly {repositoryType} {repositoryName};");
            indentWriter.WriteLineNoTabs("");

            WritePostMethod(indentWriter, resourceName, resourceVariableName, repositoryName);
            WriteGetMethod(indentWriter, resourceName, resourceVariableName, repositoryName);

            indentWriter.CloseCodeBlock();
        }

        private static void WriteClassDeclaration(IndentedTextWriter indentWriter, string resourceName)
        {
            indentWriter.WriteLine($"[Route(\"{{ {resourceName} }} \")]");
            indentWriter.WriteLine($"[ApiController]");
            indentWriter.WriteLine($"public partial class {resourceName}Controller : ControllerBase");
        }

        private static void WritePostMethod(IndentedTextWriter indentWriter, string resourceName, string resourceVariableName, string repositoryName)
        {
            indentWriter.WriteLine("[HttpPost]");
            indentWriter.WriteLine($"public async Task<IActionResult>Post{resourceName}({resourceName}Request request)");
            indentWriter.OpenCodeBlock();
            indentWriter.WriteLine($"var {resourceVariableName}Id = await {repositoryName}.Create(request.ToDomain());");
            indentWriter.WriteLine($"return Ok({resourceVariableName}Id);");
            indentWriter.CloseCodeBlock();
        }
        private static void WriteGetMethod(IndentedTextWriter indentWriter, string resourceName, string resourceVariableName, string repositoryName)
        {
            indentWriter.WriteLine("[HttpGet]");
            indentWriter.WriteLine($"[Route(\"{{{resourceVariableName}}}/{{giftCardId}}\")]");
            indentWriter.WriteLine($"public async Task<IActionResult>Get{resourceName}(int {resourceVariableName}Id)");
            indentWriter.OpenCodeBlock();
            indentWriter.WriteLine($"var {resourceVariableName} = await {repositoryName}.Get({resourceVariableName}Id);");
            indentWriter.WriteLine($"return Ok({resourceVariableName}Id);");
            indentWriter.CloseCodeBlock();
        }
    }
}
