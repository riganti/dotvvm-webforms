using System;
using System.Collections.Generic;
using System.Linq;

namespace DotVVM.Utils.AspxConverter.Parser.Tokens
{
    public abstract class TagToken : AspxToken
    {
        public string TagName { get; private set; }

        public string TagNameWithoutPrefix => TagName.Substring(TagName.LastIndexOf(":") + 1);

        public TagToken(int position, string tagName) : base(position)
        {
            TagName = tagName;
        }

        public void ChangeTagName(string newName)
        {
            TagName = newName;
        }
    }
}