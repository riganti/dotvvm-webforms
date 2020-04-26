using System;
using System.Collections.Generic;
using System.Linq;
using DotVVM.Utils.AspxConverter.Parser.Tokens;
using DotVVM.Utils.AspxConverter.Workspace;

namespace DotVVM.Utils.AspxConverter.Matching.Fixes
{
    public class RenameElementFix : FixAction
    {
        public BeginTagToken Tag { get; }
        public string NewName { get; }
        public EndTagToken EndTag { get; }

        public RenameElementFix(BeginTagToken tag, string newName, EndTagToken endTag = null)
        {
            Tag = tag;
            NewName = newName;
            EndTag = endTag;
        }

        public override void Apply(WorkspaceFixContext context)
        {
            Tag.ChangeTagName(NewName);
            EndTag?.ChangeTagName(NewName);
        }
    }
}