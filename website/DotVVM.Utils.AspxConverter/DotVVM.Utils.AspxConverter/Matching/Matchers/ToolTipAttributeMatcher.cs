using System.Collections.Generic;
using DotVVM.Utils.AspxConverter.Matching.Fixes;
using DotVVM.Utils.AspxConverter.Parser.Tokens;

namespace DotVVM.Utils.AspxConverter.Matching.Matchers
{
    public class ToolTipAttributeMatcher : AttributeMatcher
    {
        public override string MatchedAttributeName => "ToolTip";

        protected override IEnumerable<Suggestion> TryProvideSuggestions(BeginTagToken tagToken, AttributeToken attributeToken)
        {
            yield return new Suggestion()
            {
                Description = "Change <code>ToolTip</code> to <code>title</code>",
                Fixes = new FixAction[]
                {
                    new RenameAttributeFix(attributeToken, "title")
                }
            };
        }
    }
}