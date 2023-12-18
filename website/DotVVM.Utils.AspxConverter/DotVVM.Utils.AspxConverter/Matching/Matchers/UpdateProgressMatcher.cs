using System;
using System.Collections.Generic;
using System.Text;
using DotVVM.Utils.AspxConverter.Matching.Fixes;
using DotVVM.Utils.AspxConverter.Parser;
using DotVVM.Utils.AspxConverter.Parser.Tokens;

namespace DotVVM.Utils.AspxConverter.Matching.Matchers
{
    public class UpdateProgressMatcher : ControlMatcher
    {
        public override string[] MatchedTagNames => new[] { "asp:UpdateProgress", "dot:UpdateProgress" };

        protected override IEnumerable<Suggestion> TryProvideSuggestions(BeginTagToken tagToken, Func<ControlInnerElementsReader> readerFactory)
        {
            var reader = readerFactory();

            if (tagToken.TagName.StartsWith("asp:"))
            {
                yield return new Suggestion()
                {
                    Description = "Change to <code>&lt;dot:UpdateProgress&gt;</code>",
                    Fixes = new FixAction[]
                    {
                        new RenameElementFix(tagToken, "dot:UpdateProgress", reader.EndTag)
                    }
                };
            }

            if (reader.Elements.TryGetValue("ContentTemplate", out var contentTemplate))
            {
                yield return new Suggestion()
                {
                    Description = "Remove the <code>ContentTemplate</code> wrapper element",
                    Fixes = new FixAction[]
                    {
                        new RemoveTokenFix(contentTemplate[0]),
                        new RemoveTokenFix(contentTemplate[^1])
                    }
                };
            }
        }
    }
}
