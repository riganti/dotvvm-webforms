using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace DotVVM.Utils.AspxConverter.Matching.Utils
{
    public class BindingParser
    {

        public static bool TryParseBinding(string bindingExpression, out string binding)
        {
            if (bindingExpression.StartsWith("<%#:") && bindingExpression.EndsWith("%>"))
            {
                binding = bindingExpression[4..^2].Trim();
                return true;
            }
            else if (bindingExpression.StartsWith("<%#") && bindingExpression.EndsWith("%>"))
            {
                binding = bindingExpression[3..^2].Trim();
                return true;
            }

            binding = "";
            return false;
        }

        public static string TranslateBinding(string content)
        {
            try
            {
                SyntaxNode syntax = SyntaxFactory.ParseExpression(content.Trim());

                var visitor = new BindingTranslationVisitor();
                syntax = visitor.Visit(syntax);

                return syntax.NormalizeWhitespace().ToFullString();
            }
            catch
            {
#if RUN_FROM_TESTS
                throw;
#endif
            }
            return content;
        }
    }
}
