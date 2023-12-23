using DotVVM.Utils.AspxConverter.Parser.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

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
            return content.Trim();
        }
    }
}
