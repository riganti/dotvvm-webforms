using System;
using DotVVM.Utils.AspxConverter.Parser.Tokens;
using DotVVM.Utils.AspxConverter.Workspace;

namespace DotVVM.Utils.AspxConverter.Matching.Fixes
{
    public class MoveTokenRangeFix : FixAction
    {
        public AspxToken FirstToken { get; }

        public AspxToken LastToken { get; }

        public int? TokenCount { get; }

        public AspxToken TargetPosition { get; }

        public Placement Placement { get; }

        public MoveTokenRangeFix(AspxToken firstToken, int tokenCount, AspxToken targetPosition, Placement placement)
        {
            FirstToken = firstToken;
            TokenCount = tokenCount;
            TargetPosition = targetPosition;
            Placement = placement;
        }

        public MoveTokenRangeFix(AspxToken firstToken, AspxToken lastToken, AspxToken targetPosition, Placement placement)
        {
            FirstToken = firstToken;
            LastToken = lastToken;
            TargetPosition = targetPosition;
            Placement = placement;
        }

        public override void Apply(WorkspaceFixContext context)
        {
            var firstTokenIndex = context.Tokens.IndexOf(FirstToken);
            if (firstTokenIndex < 0)
            {
                throw new InvalidOperationException("Token to move not found!");
            }

            var tokenCount = TokenCount ?? context.Tokens.IndexOf(LastToken) - firstTokenIndex + 1;
            var range = context.Tokens.GetRange(firstTokenIndex, tokenCount);
            context.Tokens.RemoveRange(firstTokenIndex, tokenCount);

            var targetIndex = context.Tokens.IndexOf(TargetPosition);
            if (targetIndex < 0)
            {
                throw new InvalidOperationException("Target token not found!");
            }

            if (Placement == Placement.After)
            {
                targetIndex++;
            }
            context.Tokens.InsertRange(targetIndex, range);
        }
    }

    public enum Placement
    {
        Before,
        After
    }
}