using System;
using System.Collections.Generic;
using System.Linq;

namespace DotVVM.Utils.AspxConverter.Parser.Tokens
{
    public class EndTagToken : TagToken
    {
        public string BeforeNameFragment { get; }

        public string AfterNameFragment { get; }

        public override string Fragment => $"{BeforeNameFragment}{TagName}{AfterNameFragment}";

        public EndTagToken(int position, string beforeNameFragment, string tagName, string afterNameFragment) : base(position, tagName)
        {
            BeforeNameFragment = beforeNameFragment;
            AfterNameFragment = afterNameFragment;
        }
    }
}