using System;
using System.Collections.Generic;
using System.Text;
using DotVVM.Utils.AspxConverter.Matching.Fixes;
using DotVVM.Utils.AspxConverter.Parser;
using DotVVM.Utils.AspxConverter.Parser.Tokens;

namespace DotVVM.Utils.AspxConverter.Matching.Matchers
{
    public class GridViewMatcher : ControlMatcher
    {
        public override string[] MatchedTagNames => new[] { "asp:GridView" };

        protected override IEnumerable<Suggestion> TryProvideSuggestions(BeginTagToken tagToken, Func<ControlInnerElementsReader> readerFactory)
        {
            var reader = readerFactory();

            yield return new Suggestion()
            {
                Description = "Change to <code>&lt;dot:GridView&gt;</code>",
                Fixes = new FixAction[]
                {
                    new RenameElementFix(tagToken, "dot:GridView", reader.EndTag)
                }
            };
        }
    }
}
