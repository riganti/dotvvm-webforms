using System;
using System.Collections.Generic;
using System.Linq;

namespace DotVVM.Utils.AspxConverter.Parser.Tokens
{
    public class BeginTagToken : TagToken
    {
        public string BeforeNameFragment { get; }

        public bool IsSelfClosing { get; }
        
        public List<AttributeToken> Attributes { get; }

        public string AfterAttributesFragment { get; }

        public override string Fragment => $"{BeforeNameFragment}{TagName}{string.Join(string.Empty, Attributes.Select(a => a.Fragment))}{AfterAttributesFragment}";

        public BeginTagToken(int position, string beforeNameFragment, string tagName, bool isSelfClosing, List<AttributeToken> attributes, string afterAttributesFragment) : base(position, tagName)
        {
            BeforeNameFragment = beforeNameFragment;
            IsSelfClosing = isSelfClosing;
            Attributes = attributes;
            AfterAttributesFragment = afterAttributesFragment;
        }

        public AttributeToken FindAttribute(string name)
        {
            return Attributes.FirstOrDefault(a => string.Equals(a.Name, name, StringComparison.CurrentCultureIgnoreCase));
        }

        public string FindOrGenerateId()
        {
            return FindAttribute("ID")?.GetValue() ?? ("Unnamed" + TagNameWithoutPrefix);
        }

        public void AddOrUpdateAttribute(string name, AttributeValueToken value)
        {
            var attribute = FindAttribute(name);
            if (attribute == null)
            {
                attribute = new AttributeToken(0, " ", name, "=", value);
                Attributes.Add(attribute);
            }
            else
            {
                attribute.ChangeValue(value);
            }

        }
    }
}