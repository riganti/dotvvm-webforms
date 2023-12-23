using DotVVM.Utils.AspxConverter.Parser.Tokens;
using DotVVM.Utils.AspxConverter.Workspace;
using System;

namespace DotVVM.Utils.AspxConverter.Matching.Fixes
{
    public class ReplaceTokenFix : FixAction
    {
        private AspxToken replacedToken;
        private int replacedTokensCount;
        private AspxToken[] newTokens;

        public ReplaceTokenFix(AspxToken replacedToken, params AspxToken[] newTokens)
            : this(replacedToken, 1, newTokens)
        {
        }

        public ReplaceTokenFix(AspxToken replacedToken, int replacedTokensCount, params AspxToken[] newTokens)
        {
            this.replacedToken = replacedToken;
            this.replacedTokensCount = replacedTokensCount;
            this.newTokens = newTokens;
        }

        public override void Apply(WorkspaceFixContext context)
        {
            var tokenIndex = context.Tokens.IndexOf(replacedToken);
            if (tokenIndex < 0)
            {
                throw new InvalidOperationException("The token was already removed!");
            }
            if (tokenIndex + replacedTokensCount > context.Tokens.Count)
            {
                throw new InvalidOperationException("The token range is longer than the token list!");
            }

            context.Tokens.RemoveRange(tokenIndex, replacedTokensCount);
            context.Tokens.InsertRange(tokenIndex, newTokens);
        }
    }
}