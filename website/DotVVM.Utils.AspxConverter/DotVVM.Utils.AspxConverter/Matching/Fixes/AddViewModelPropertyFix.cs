using System;
using System.Collections.Generic;
using System.Linq;
using DotVVM.Utils.AspxConverter.Workspace;
using DotVVM.Utils.AspxConverter.Workspace.CodeGen;

namespace DotVVM.Utils.AspxConverter.Matching.Fixes
{
    public class AddViewModelPropertyFix : FixAction
    {
        public string Name { get; }
        public string Type { get; }

        public AddViewModelPropertyFix(string name, string type)
        {
            Name = name;
            Type = type;
        }

        public override void Apply(WorkspaceFixContext context)
        {
            var file = context.GetViewModelFile();

            file.AddOrExtendItem(new CsharpItem()
            {
                ItemType = CsharpItemType.PublicProperty,
                Name = Name,
                Type = Type
            });
        }
    }
}