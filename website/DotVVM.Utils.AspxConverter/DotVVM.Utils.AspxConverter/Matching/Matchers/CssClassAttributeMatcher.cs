using DotVVM.Utils.AspxConverter.Matching.Fixes;
using DotVVM.Utils.AspxConverter.Parser.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotVVM.Utils.AspxConverter.Matching.Matchers
{
    public class CssClassAttributeMatcher : AttributeMatcher
    {
        public override string MatchedAttributeName => "CssClass";

        protected override IEnumerable<Suggestion> TryProvideSuggestions(BeginTagToken tagToken, AttributeToken attributeToken)
        {
            yield return new Suggestion()
            {
                Description = "Change <code>CssClass</code> to <code>class</code>",
                Fixes = new FixAction[]
                {
                    new RenameAttributeFix(attributeToken, "class")
                }
            };
        }
    }
}
