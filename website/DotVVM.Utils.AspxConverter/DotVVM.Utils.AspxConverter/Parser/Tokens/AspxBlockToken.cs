using System;
using System.Collections.Generic;
using System.Linq;

namespace DotVVM.Utils.AspxConverter.Parser.Tokens
{
    public abstract class AspxBlockToken : AspxToken
    {
        public string OpenBraceFragment { get; }

        public string ExpressionTypeFragment { get; }

        public string Content { get; }

        public string CloseBraceFragment { get; }

        public override string Fragment => $"{OpenBraceFragment}{ExpressionTypeFragment}{Content}{CloseBraceFragment}";


        public AspxBlockToken(int position, string openBraceFragment, string expressionTypeFragment, string content, string closeBraceFragment) : base(position)
        {
            OpenBraceFragment = openBraceFragment;
            ExpressionTypeFragment = expressionTypeFragment;
            Content = content;
            CloseBraceFragment = closeBraceFragment;
        }
    }
}