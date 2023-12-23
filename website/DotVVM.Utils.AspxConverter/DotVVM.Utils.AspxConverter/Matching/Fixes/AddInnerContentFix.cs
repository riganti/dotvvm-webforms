﻿using DotVVM.Utils.AspxConverter.Matching.Utils;
using DotVVM.Utils.AspxConverter.Parser.Tokens;
using DotVVM.Utils.AspxConverter.Workspace;

namespace DotVVM.Utils.AspxConverter.Matching.Fixes
{
    public class AddInnerContentFix : FixAction
    {
        public BeginTagToken BeginTag { get; }

        public EndTagToken EndTag { get; }

        public string Content { get; }

        public bool EnsureDoubleBraceBinding { get; }

        public AddInnerContentFix(BeginTagToken beginTag, EndTagToken endTag, string content, bool ensureDoubleBraceBinding)
        {
            BeginTag = beginTag;
            EndTag = endTag;
            Content = content;
            EnsureDoubleBraceBinding = ensureDoubleBraceBinding;
        }

        public override void Apply(WorkspaceFixContext context)
        {
            var targetIndex = context.Tokens.IndexOf(BeginTag);
            var contentNode = new LiteralToken(0, EnsureDoubleBraceBinding ? BindingParser.EnsureDoubleBraceBinding(Content) : Content);

            if (BeginTag.IsSelfClosing)
            {
                var beginTag = new BeginTagToken(BeginTag.Position, "<", BeginTag.TagName, false, BeginTag.Attributes, ">");
                var endTag = new EndTagToken(0, "</", BeginTag.TagName, ">");
                
                context.Tokens.RemoveAt(targetIndex);
                context.Tokens.InsertRange(targetIndex, new AspxToken [] { beginTag, contentNode, endTag });
            }
            else
            {
                context.Tokens.Insert(targetIndex + 1, contentNode);
            }
        }
    }
}