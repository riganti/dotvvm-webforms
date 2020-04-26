using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using DotVVM.Utils.AspxConverter.Workspace.CodeGen;

namespace DotVVM.Utils.AspxConverter.Workspace
{
    public class CsharpFileBuilder
    {

        public string ClassName { get; set; }

        public List<CsharpItem> Items { get; set; } = new List<CsharpItem>();

        public string Code => GenerateCode();

        public void AddOrExtendItem(CsharpItem item)
        {
            if (item.ItemType == CsharpItemType.PublicMethod)
            {
                // append code to a public method body
                var existingMethod = Items.FirstOrDefault(m =>
                    m.ItemType == CsharpItemType.PublicMethod
                    && m.Name == item.Name
                    && m.Type == item.Type);
                if (existingMethod != null)
                {
                    existingMethod.Body.Add(string.Empty);
                    existingMethod.Body.AddRange(item.Body);
                    return;
                }
            }
            else if (item.ItemType == CsharpItemType.PublicProperty)
            {
                // don't add duplicate properties
                var existingProperty = Items.FirstOrDefault(p =>
                    p.ItemType == CsharpItemType.PublicProperty
                    && p.Name == item.Name
                    && p.Type == item.Type
                );
                if (existingProperty != null)
                {
                    return;
                }
            }

            // otherwise add it as a new item
            Items.Add(item);
        }

        private string GenerateCode()
        {
            return $@"public class {ClassName}
{{

{CodeGenUtils.Indent(1, string.Join("\r\n", Items.Select(i => i.GenerateCode())))}

}}";
        }
    }
}