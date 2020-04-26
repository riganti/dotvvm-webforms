using System;
using System.Collections.Generic;
using System.Linq;
using DotVVM.Utils.AspxConverter.Matching.Fixes;
using DotVVM.Utils.AspxConverter.Parser;
using DotVVM.Utils.AspxConverter.Parser.Tokens;

namespace DotVVM.Utils.AspxConverter.Matching.Matchers
{
    public class HyperLinkMatcher : ControlMatcher
    {
        public override string[] MatchedTagNames => new[] { "asp:HyperLink" };

        protected override IEnumerable<Suggestion> TryProvideSuggestions(BeginTagToken tagToken, Func<ControlInnerElementsReader> readerFactory)
        {
            var reader = readerFactory();

            yield return new Suggestion()
            {
                Description = "Change to <code>&lt;a&gt;</code>",
                Fixes = new FixAction[]
                {
                    new RenameElementFix(tagToken, "a", reader.EndTag)
                }
            };

            if (tagToken.FindAttribute("NavigateUrl") is { } navigateUrl)
            {
                yield return new Suggestion()
                {
                    Description = "Change <code>NavigateUrl</code> to <code>href</code>",
                    Fixes = new FixAction[]
                    {
                        new RenameAttributeFix(navigateUrl, "a")
                    }
                };
            }
        }
    }
}