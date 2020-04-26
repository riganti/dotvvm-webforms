using System;
using System.Collections.Generic;
using System.Linq;

namespace DotVVM.Utils.AspxConverter.Parser.Tokens
{
    public class DirectiveToken : AspxBlockToken
    {
        public DirectiveToken(int position, string openBraceFragment, string expressionTypeFragment, string content, string closeBraceFragment) : base(position, openBraceFragment, expressionTypeFragment, content, closeBraceFragment)
        {
        }
    }
}