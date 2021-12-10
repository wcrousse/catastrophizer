using System.CodeDom.Compiler;

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
    }
}
