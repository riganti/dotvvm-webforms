using System;
using DotVVM.Utils.AspxConverter.Parser.Tokens;
using DotVVM.Utils.AspxConverter.Workspace;

namespace DotVVM.Utils.AspxConverter.Matching.Fixes
{
    public class RemoveTokenRangeFix : FixAction
    {
        public AspxToken FirstToken { get; }

        public AspxToken LastToken { get; }

        public int? TokenCount { get; }

        public RemoveTokenRangeFix(AspxToken firstToken, int tokenCount)
        {
            FirstToken = firstToken;
            TokenCount = tokenCount;
        }

        public RemoveTokenRangeFix(AspxToken firstToken, AspxToken lastToken)
        {
            FirstToken = firstToken;
            LastToken = lastToken;
        }

        public override void Apply(WorkspaceFixContext context)
        {
            var firstTokenIndex = context.Tokens.IndexOf(FirstToken);
            if (firstTokenIndex < 0)
            {
                throw new InvalidOperationException("Token to remove not found!");
            }

            var tokenCount = TokenCount ?? context.Tokens.IndexOf(LastToken) - firstTokenIndex + 1;
            context.Tokens.RemoveRange(firstTokenIndex, tokenCount);
        }
    }
}