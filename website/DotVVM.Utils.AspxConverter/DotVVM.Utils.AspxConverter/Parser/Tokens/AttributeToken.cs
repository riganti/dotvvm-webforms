using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DotVVM.Utils.AspxConverter.Parser.Tokens
{
    public class AttributeToken : AspxToken 
    {
        public string BeforeNameFragment { get; }

        public string Name { get; private set; }

        public string BeforeValueFragment { get; }

        public AttributeValueToken Value { get; private set; }

        public override string Fragment => $"{BeforeNameFragment}{Name}{BeforeValueFragment}{Value.Fragment}";


        public AttributeToken(int position, string beforeNameFragment, string name, string beforeValueFragment, AttributeValueToken value) : base(position)
        {
            BeforeNameFragment = beforeNameFragment;
            Name = name;
            BeforeValueFragment = beforeValueFragment;
            Value = value;
        }

        public string GetValue()
        {
            if (Value is AttributeEmptyValueToken)
            {
                return Name;
            }
            else if (Value is AttributeUnquotedValueToken unquotedToken)
            {
                return unquotedToken.Value;
            }
            else if (Value is AttributeQuotedValueToken quotedToken)
            {
                return quotedToken.Value;
            }
            else
            {
                return Value.Fragment;
            }
        }

        public void ChangeName(string newName)
        {
            Name = newName;
        }

        public void ChangeValue(AttributeValueToken value)
        {
            Value = value;
        }
    }
}