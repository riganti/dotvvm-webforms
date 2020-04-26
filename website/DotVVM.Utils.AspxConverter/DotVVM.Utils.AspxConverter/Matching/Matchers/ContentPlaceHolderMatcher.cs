using System;
using System.Collections.Generic;
using System.Text;
using DotVVM.Utils.AspxConverter.Matching.Fixes;
using DotVVM.Utils.AspxConverter.Parser;
using DotVVM.Utils.AspxConverter.Parser.Tokens;

namespace DotVVM.Utils.AspxConverter.Matching.Matchers
{
    public class ContentPlaceHolderMatcher : ControlMatcher
    {
        public override string[] MatchedTagNames => new[] { "asp:ContentPlaceHolder" };

        protected override IEnumerable<Suggestion> TryProvideSuggestions(BeginTagToken tagToken, Func<ControlInnerElementsReader> readerFactory)
        {
            var reader = readerFactory();

            yield return new Suggestion()
            {
                Description = "Change to <code>&lt;dot:ContentPlaceHolder&gt;</code>",
                Fixes = new FixAction[]
                {
                    new RenameElementFix(tagToken, "dot:ContentPlaceHolder", reader.EndTag)
                }
            };
        }
    }
}
