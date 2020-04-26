using System;
using System.Collections.Generic;
using System.Linq;

namespace DotVVM.Utils.AspxConverter.Parser.Tokens
{
    public class CodeBlockToken : AspxBlockToken
    {
        public CodeBlockToken(int position, string openBraceFragment, string content, string closeBraceFragment) : base(position, openBraceFragment, string.Empty, content, closeBraceFragment)
        {
        }
    }
}