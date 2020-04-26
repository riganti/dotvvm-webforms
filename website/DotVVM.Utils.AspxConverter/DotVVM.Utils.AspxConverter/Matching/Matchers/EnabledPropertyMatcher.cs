using System;
using System.Collections.Generic;
using System.Linq;
using DotVVM.Utils.AspxConverter.Matching.Fixes;
using DotVVM.Utils.AspxConverter.Parser.Tokens;

namespace DotVVM.Utils.AspxConverter.Matching.Matchers
{
    public class EnabledPropertyMatcher : AttributeMatcher
    {
        public override string MatchedAttributeName => "Enabled";

        protected override IEnumerable<Suggestion> TryProvideSuggestions(BeginTagToken tagToken, AttributeToken attributeToken)
        {
            if (attributeToken.GetValue().StartsWith("{value"))
            {
                yield break;
            }

            var enabledProperty = tagToken.FindOrGenerateId() + "Enabled";
            yield return new Suggestion()
            {
                Description = "Add viewmodel property to guide the <code>Enabled</code> state",
                Fixes = new FixAction[]
                {
                    new SetAttributeValueBindingFix(tagToken, "Enabled", enabledProperty),
                    new AddViewModelPropertyFix(enabledProperty, "bool")
                }
            };
        }
    }
}