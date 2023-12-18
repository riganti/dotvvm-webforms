using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotVVM.Utils.AspxConverter.Matching.Fixes;
using DotVVM.Utils.AspxConverter.Parser;
using DotVVM.Utils.AspxConverter.Parser.Tokens;

namespace DotVVM.Utils.AspxConverter.Matching.Matchers
{
    public class RepeaterMatcher : ControlMatcher
    {
        public override string[] MatchedTagNames => new [] { "asp:Repeater", "dot:Repeater" };

        protected override IEnumerable<Suggestion> TryProvideSuggestions(BeginTagToken tagToken, Func<ControlInnerElementsReader> readerFactory)
        {
            var reader = readerFactory();

            if (tagToken.TagName.StartsWith("asp:"))
            {
                yield return new Suggestion()
                {
                    Description = "Change to <code>&lt;dot:Repeater&gt;</code>",
                    Fixes = new FixAction[]
                    {
                        new RenameElementFix(tagToken, "dot:Repeater", reader.EndTag)
                    }
                };
            }

            if (reader.Elements.TryGetValue("HeaderTemplate", out var headerTemplate))
            {
                yield return new Suggestion()
                {
                    Description = "Move <code>HeaderTemplate</code> before the control",
                    Fixes = new FixAction[]
                    {
                        new MoveTokenRangeFix(headerTemplate.ElementAt(1), headerTemplate.Count - 2, tagToken, Placement.Before),
                        new RemoveTokenFix(headerTemplate[0]),
                        new RemoveTokenFix(headerTemplate[^1])
                    }
                };
            }

            if (reader.EndTag != null && reader.Elements.TryGetValue("FooterTemplate", out var footerTemplate))
            {
                yield return new Suggestion()
                {
                    Description = "Move <code>FooterTemplate</code> after the control",
                    Fixes = new FixAction[]
                    {
                        new MoveTokenRangeFix(footerTemplate.ElementAt(1), footerTemplate.Count - 2, reader.EndTag, Placement.After),
                        new RemoveTokenFix(footerTemplate[0]),
                        new RemoveTokenFix(footerTemplate[^1])
                    }
                };
            }
        }
    }
}
