using System;
using System.Collections.Generic;
using System.Linq;

namespace DotVVM.Utils.AspxConverter.Parser.Tokens
{
    public class AttributeUnquotedValueToken : AttributeValueToken
    {

        public string Value { get; }

        public override string Fragment => Value;

        public AttributeUnquotedValueToken(int position, string value) : base(position)
        {
            Value = value;
        }
    }
}