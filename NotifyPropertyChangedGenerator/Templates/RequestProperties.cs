using Microsoft.CodeAnalysis;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace CatastrofizerGenerator.Templates
{
    public static class RequestProperties
    {
        public static void Generate(IEnumerable<IPropertySymbol> properties, IndentedTextWriter indentWriter)
        {
            foreach (var propertySymbol in properties)
            {
                var propertyName = propertySymbol.Name;
                indentWriter.WriteLine($"public {propertySymbol.Type} {propertyName} {{ get; set; }} ");
            }
        }
    }
}
