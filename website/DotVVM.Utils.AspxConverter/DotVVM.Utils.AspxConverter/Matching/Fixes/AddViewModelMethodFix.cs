using System;
using System.Collections.Generic;
using System.Linq;
using DotVVM.Utils.AspxConverter.Workspace;
using DotVVM.Utils.AspxConverter.Workspace.CodeGen;

namespace DotVVM.Utils.AspxConverter.Matching.Fixes
{
    public class AddViewModelMethodFix : FixAction
    {
        public string EventName { get; }
        public string ReturnType { get; }
        public string Body { get; }

        public AddViewModelMethodFix(string eventName, string returnType, string body)
        {
            EventName = eventName;
            ReturnType = returnType;
            Body = body;
        }

        public override void Apply(WorkspaceFixContext context)
        {
            var file = context.GetViewModelFile();

            file.AddOrExtendItem(new CsharpItem()
            {
                ItemType = CsharpItemType.PublicMethod,
                Name = EventName, 
                Type = ReturnType,
                Body = new List<string>()
                {
                    Body
                }
            });
        }
    }
}