using System;
using System.Collections.Generic;
using System.Linq;

namespace DotVVM.Utils.AspxConverter.Parser.Tokens
{
    public class AttributeQuotedValueToken : AttributeValueToken
    {
        public string Value { get; }
        public string QuoteChar { get; }
        public override string Fragment => $"{QuoteChar}{Value}{QuoteChar}";

        public AttributeQuotedValueToken(int position, string quoteChar, string value) : base(position)
        {
            QuoteChar = quoteChar;
            Value = value;
        }
    }
}