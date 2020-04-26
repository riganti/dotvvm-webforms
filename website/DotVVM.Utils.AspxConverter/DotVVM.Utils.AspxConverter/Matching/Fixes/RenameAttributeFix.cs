using DotVVM.Utils.AspxConverter.Parser.Tokens;
using DotVVM.Utils.AspxConverter.Workspace;

namespace DotVVM.Utils.AspxConverter.Matching.Fixes
{
    public class RenameAttributeFix : FixAction
    {
        public AttributeToken Attribute { get; }

        public string NewName { get; }

        public RenameAttributeFix(AttributeToken attribute, string newName)
        {
            Attribute = attribute;
            NewName = newName;
        }

        public override void Apply(WorkspaceFixContext context)
        {
            Attribute.ChangeName(NewName);
        }
    }
}