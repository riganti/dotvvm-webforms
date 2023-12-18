using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotVVM.Utils.AspxConverter.Matching.Fixes;
using DotVVM.Utils.AspxConverter.Parser;
using DotVVM.Utils.AspxConverter.Parser.Tokens;

namespace DotVVM.Utils.AspxConverter.Matching.Matchers
{
    public class ListViewMatcher : ControlMatcher
    {
        public override string[] MatchedTagNames => new[] { "asp:ListView", "dot:Repeater" };

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

            if (reader.Elements.TryGetValue("LayoutTemplate", out var layoutTemplate))
            {
                // find the placeholder start tag
                var placeholderControlId = tagToken.FindAttribute("ItemPlaceholderID")?.GetValue() ?? "ItemPlaceHolder";
                var placeholderBeginTag = layoutTemplate.OfType<BeginTagToken>().FirstOrDefault(t => t.FindAttribute("ID")?.GetValue() == placeholderControlId);
                if (placeholderBeginTag != null)
                {
                    // find the placeholder end tag
                    var placeholderReader = reader.CreateChildReader(placeholderBeginTag);
                    var placeholderEndTag = (AspxToken)placeholderReader.EndTag ?? placeholderBeginTag;

                    yield return new Suggestion()
                    {
                        Description = "Move <code>LayoutTemplate</code> outside",
                        Fixes = new FixAction[]
                        {
                            new MoveTokenRangeFix(layoutTemplate.ElementAt(1), layoutTemplate.Count - 2, tagToken, Placement.Before),
                            new RemoveTokenFix(layoutTemplate[0]),
                            new RemoveTokenFix(layoutTemplate[^1]),
                            new MoveTokenRangeFix(tagToken, reader.EndTag, placeholderBeginTag, Placement.Before),
                            new RemoveTokenRangeFix(placeholderBeginTag, placeholderEndTag)
                        }
                    };
                }
            }
        }
    }
}
