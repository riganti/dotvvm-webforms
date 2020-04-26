using System;
using System.Collections.Generic;
using System.Linq;
using DotVVM.Utils.AspxConverter.Parser.Tokens;
using DotVVM.Utils.AspxConverter.Workspace;

namespace DotVVM.Utils.AspxConverter.Matching.Fixes
{
    public class RemoveTokenFix : FixAction
    {
        public AspxToken Token { get; }

        public RemoveTokenFix(AspxToken token)
        {
            Token = token;
        }

        public override void Apply(WorkspaceFixContext context)
        {
            if (!context.Tokens.Remove(Token))
            {
                throw new InvalidOperationException("The token was already removed!");
            }
        }
    }
}