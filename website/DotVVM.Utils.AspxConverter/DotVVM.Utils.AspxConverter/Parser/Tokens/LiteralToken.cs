using System;
using System.Collections.Generic;
using System.Linq;

namespace DotVVM.Utils.AspxConverter.Parser.Tokens
{
    public class LiteralToken : AspxToken
    {
        public string Text { get; }

        public override string Fragment => Text;

        public LiteralToken(int position, string text) : base(position)
        {
            Text = text;
        }
    }
}