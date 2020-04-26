using System;
using System.Collections.Generic;
using System.Linq;

namespace DotVVM.Utils.AspxConverter.Parser.Tokens
{
    public abstract class AttributeValueToken : AspxToken
    {
        public AttributeValueToken(int position) : base(position)
        {
        }
    }
}