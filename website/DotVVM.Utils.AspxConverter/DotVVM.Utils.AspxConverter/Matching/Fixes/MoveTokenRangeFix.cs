using System;
using DotVVM.Utils.AspxConverter.Parser.Tokens;
using DotVVM.Utils.AspxConverter.Workspace;

namespace DotVVM.Utils.AspxConverter.Matching.Fixes
{
    public class MoveTokenRangeFix : FixAction
    {
        public AspxToken FirstToken { get; }

        public int TokenCount { get; }

        public AspxToken TargetPosition { get; }

        public Placement Placement { get; }

        public MoveTokenRangeFix(AspxToken firstToken, int tokenCount, AspxToken targetPosition, Placement placement)
        {
            FirstToken = firstToken;
            TokenCount = tokenCount;
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

            var range = context.Tokens.GetRange(firstTokenIndex, TokenCount);
            context.Tokens.RemoveRange(firstTokenIndex, TokenCount);

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