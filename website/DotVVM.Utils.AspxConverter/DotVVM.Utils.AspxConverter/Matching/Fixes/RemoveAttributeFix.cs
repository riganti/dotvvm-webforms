using System;
using System.Collections.Generic;
using System.Linq;
using DotVVM.Utils.AspxConverter.Parser.Tokens;
using DotVVM.Utils.AspxConverter.Workspace;

namespace DotVVM.Utils.AspxConverter.Matching.Fixes
{
    public class RemoveAttributeFix : FixAction
    {
        public BeginTagToken Tag { get; }

        public AttributeToken Attribute { get; }

        public RemoveAttributeFix(BeginTagToken tag, AttributeToken attribute)
        {
            Tag = tag;
            Attribute = attribute;
        }

        public override void Apply(WorkspaceFixContext context)
        {
            if (!Tag.Attributes.Remove(Attribute))
            {
                throw new InvalidOperationException("The attribute doesn't belong to the tag.");
            }
        }
    }
}