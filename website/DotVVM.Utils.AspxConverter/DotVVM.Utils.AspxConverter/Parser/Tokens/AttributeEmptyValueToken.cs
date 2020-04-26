using System;
using System.Collections.Generic;
using System.Linq;

namespace DotVVM.Utils.AspxConverter.Parser.Tokens
{
    public class AttributeEmptyValueToken : AttributeValueToken
    {
        public override string Fragment => string.Empty;

        public AttributeEmptyValueToken(int position) : base(position)
        {
        }
    }
}