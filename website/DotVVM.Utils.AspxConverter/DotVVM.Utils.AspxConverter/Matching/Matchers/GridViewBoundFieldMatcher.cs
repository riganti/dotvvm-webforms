using System;
using System.Collections.Generic;
using System.Linq;
using DotVVM.Utils.AspxConverter.Matching.Fixes;
using DotVVM.Utils.AspxConverter.Parser;
using DotVVM.Utils.AspxConverter.Parser.Tokens;

namespace DotVVM.Utils.AspxConverter.Matching.Matchers
{
    public class GridViewBoundFieldMatcher : ControlMatcher
    {
        public override string[] MatchedTagNames => new[] { "asp:BoundField", "dot:GridViewTextColumn" };

        protected override IEnumerable<Suggestion> TryProvideSuggestions(BeginTagToken tagToken, Func<ControlInnerElementsReader> readerFactory)
        {
            var reader = readerFactory();

            if (tagToken.TagName.StartsWith("asp:"))
            {
                yield return new Suggestion()
                {
                    Description = "Change to <code>&lt;dot:GridViewTextColumn&gt;</code>",
                    Fixes = new FixAction[]
                    {
                        new RenameElementFix(tagToken, "dot:GridViewTextColumn", reader.EndTag)
                    }
                };
            }

            if (tagToken.FindAttribute("DataField") is { } dataField)
            {
                yield return new Suggestion()
                {
                    Description = "Change <code>DataField</code> to <code>ValueBinding</code>",
                    Fixes = new FixAction[]
                    {
                        new RenameAttributeFix(dataField, "ValueBinding"), 
                        new SetAttributeValueBindingFix(tagToken, "ValueBinding", dataField.GetValue()), 
                    }
                };
            }
            
        }
    }
}