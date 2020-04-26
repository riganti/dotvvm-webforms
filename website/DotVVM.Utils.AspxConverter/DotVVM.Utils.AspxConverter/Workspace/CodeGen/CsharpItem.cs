using System;
using System.Collections.Generic;
using System.Linq;

namespace DotVVM.Utils.AspxConverter.Workspace.CodeGen
{
    public class CsharpItem
    {

        public CsharpItemType ItemType { get; set; }

        public string Type { get; set; }

        public string Name { get; set; }

        public List<string> Body { get; set; }

        public string GenerateCode()
        {
            switch (ItemType)
            {
                case CsharpItemType.PublicProperty:
                    return $"public {Type} {Name} {{ get; set; }}";

                case CsharpItemType.PublicMethod:
                    return $@"public {Type} {Name}() 
{{
{CodeGenUtils.Indent(1, string.Join("\r\n", Body))}
}}";

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}