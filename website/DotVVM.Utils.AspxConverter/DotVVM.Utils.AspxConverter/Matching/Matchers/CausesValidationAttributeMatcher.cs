using DotVVM.Utils.AspxConverter.Matching.Fixes;
using DotVVM.Utils.AspxConverter.Parser.Tokens;
using System.Collections.Generic;

namespace DotVVM.Utils.AspxConverter.Matching.Matchers
{
    public class CausesValidationAttributeMatcher : AttributeMatcher
    {
        public override string MatchedAttributeName => "CausesValidation";

        protected override IEnumerable<Suggestion> TryProvideSuggestions(BeginTagToken tagToken, AttributeToken attributeToken)
        {
            yield return new Suggestion()
            {
                Description = "Change <code>CausesValidation</code> to <code>Validation.Enabled</code>",
                Fixes = new FixAction[]
                {
                    new RenameAttributeFix(attributeToken, "Validation.Enabled")
                }
            };
        }
    }
}
