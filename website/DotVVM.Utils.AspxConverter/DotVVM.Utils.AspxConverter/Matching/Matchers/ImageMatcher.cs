using System;
using System.Collections.Generic;
using System.Linq;
using DotVVM.Utils.AspxConverter.Matching.Fixes;
using DotVVM.Utils.AspxConverter.Parser;
using DotVVM.Utils.AspxConverter.Parser.Tokens;

namespace DotVVM.Utils.AspxConverter.Matching.Matchers
{
    public class ImageMatcher : ControlMatcher
    {
        public override string[] MatchedTagNames => new[] { "asp:Image", "img" };

        protected override IEnumerable<Suggestion> TryProvideSuggestions(BeginTagToken tagToken, Func<ControlInnerElementsReader> readerFactory)
        {
            var reader = readerFactory();

            if (tagToken.TagName.StartsWith("asp:"))
            {
                yield return new Suggestion()
                {
                    Description = "Change to <code>&lt;img&gt;</code>",
                    Fixes = new FixAction[]
                    {
                        new RenameElementFix(tagToken, "img", reader.EndTag)
                    }
                };
            }

            if (tagToken.FindAttribute("ImageUrl") is { } imageUrl)
            {
                yield return new Suggestion()
                {
                    Description = "Change <code>ImageUrl</code> to <code>src</code>",
                    Fixes = new FixAction[]
                    {
                        new RenameAttributeFix(imageUrl, "src")
                    }
                };
            }

            if (tagToken.FindAttribute("AlternateText") is { } alternateText)
            {
                yield return new Suggestion()
                {
                    Description = "Change <code>AlternateText</code> to <code>alt</code>",
                    Fixes = new FixAction[]
                    {
                        new RenameAttributeFix(alternateText, "alt")
                    }
                };
            }
        }
    }
}