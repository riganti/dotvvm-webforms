using DotVVM.Utils.AspxConverter.Parser.Tokens;
using DotVVM.Utils.AspxConverter.Workspace;

namespace DotVVM.Utils.AspxConverter.Matching.Fixes
{
    public class SetAttributeValueFix : FixAction
    {
        public BeginTagToken Tag { get; }
        public string Name { get; }
        public string Value { get; }

        public SetAttributeValueFix(BeginTagToken tag, string name, string value)
        {
            Tag = tag;
            Name = name;
            Value = value;
        }

        public override void Apply(WorkspaceFixContext context)
        {
            var attributeValue = new AttributeQuotedValueToken(0, "\"", Value);

            Tag.AddOrUpdateAttribute(Name, attributeValue);
        }
    }
}