using System;
using System.Collections.Generic;
using System.Linq;
using DotVVM.Utils.AspxConverter.Parser.Tokens;
using DotVVM.Utils.AspxConverter.Workspace;

namespace DotVVM.Utils.AspxConverter.Matching.Fixes
{
    public class SetAttributeCommandBindingFix : FixAction
    {
        public BeginTagToken Tag { get; }
        public string Name { get; }
        public string CommandName { get; }

        public SetAttributeCommandBindingFix(BeginTagToken tag, string name, string commandName)
        {
            Tag = tag;
            Name = name;
            CommandName = commandName;
        }

        public override void Apply(WorkspaceFixContext context)
        {
            var commandBinding = "{command: " + CommandName + "()}";
            var attributeValue = new AttributeQuotedValueToken(0, "\"", commandBinding);

            Tag.AddOrUpdateAttribute(Name, attributeValue);
        }
    }
}