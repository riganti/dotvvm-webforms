using System;
using System.Collections.Generic;
using System.Linq;
using DotVVM.Utils.AspxConverter.Parser.Tokens;
using DotVVM.Utils.AspxConverter.Workspace;

namespace DotVVM.Utils.AspxConverter.Matching.Fixes
{
    public class SetAttributeValueBindingFix : FixAction
    {
        public BeginTagToken Tag { get; }
        public string Name { get; }
        public string ViewModelProperty { get; }

        public SetAttributeValueBindingFix(BeginTagToken tag, string name, string viewModelProperty)
        {
            Tag = tag;
            Name = name;
            ViewModelProperty = viewModelProperty;
        }

        public override void Apply(WorkspaceFixContext context)
        {
            var commandBinding = "{value: " + ViewModelProperty + "}";
            var value = new AttributeQuotedValueToken(0, "\"", commandBinding);

            Tag.AddOrUpdateAttribute(Name, value);
        }
    }
}