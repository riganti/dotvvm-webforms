using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotVVM.Utils.AspxConverter.Matching.Fixes;
using DotVVM.Utils.AspxConverter.Parser.Tokens;

namespace DotVVM.Utils.AspxConverter.Matching.Matchers
{
    public class RunatServerMatcher : AttributeMatcher
    {
        public override string MatchedAttributeName => "runat";

        protected override IEnumerable<Suggestion> TryProvideSuggestions(BeginTagToken tagToken, AttributeToken attributeToken)
        {
            if (string.Equals(attributeToken.GetValue(), "server", StringComparison.CurrentCultureIgnoreCase))
            {
                yield return new Suggestion()
                {
                    Description = @"DotVVM doesn't need <code>runat=""server""</code> attributes.",
                    Fixes = new[]
                    {
                        new RemoveAttributeFix(tagToken, attributeToken)
                    }
                };
            }
        }
    }
}
