using System.CodeDom.Compiler;
using System.Linq;

namespace CatastrofizerGenerator
{
    public static class IndentWriterExtensions
    {
        public static void OpenCodeBlock(this IndentedTextWriter indentWriter)
        {
            indentWriter.WriteLine("{");
            indentWriter.Indent++;
        }
        public static void CloseCodeBlock(this IndentedTextWriter indentWriter)
        {
            indentWriter.Indent--;
            indentWriter.WriteLine("}");
        }

        public static string ToCamelCase(this string txt)
        {
            var c = txt.ToLower().First();
            return c + txt.Substring(1);
        }
    }
}
