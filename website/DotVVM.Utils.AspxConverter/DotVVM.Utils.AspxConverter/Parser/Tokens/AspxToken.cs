using System;
using System.Collections.Generic;
using System.Linq;

namespace DotVVM.Utils.AspxConverter.Parser.Tokens
{
    public abstract class AspxToken
    {
        public int Position { get; }

        public abstract string Fragment { get; }

        public int Length => Fragment.Length;

        public AspxToken(int position)
        {
            Position = position;
        }
    }
}