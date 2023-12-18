using System;
using System.Collections.Generic;
using System.Linq;
using DotVVM.Utils.AspxConverter.Matching.Fixes;
using DotVVM.Utils.AspxConverter.Parser;
using DotVVM.Utils.AspxConverter.Parser.Tokens;

namespace DotVVM.Utils.AspxConverter.Matching.Matchers
{
    public class GridViewTemplateFieldMatcher : ControlMatcher
    {
        public override string[] MatchedTagNames => new[] { "asp:TemplateField", "dot:GridViewTemplateColumn" };

        protected override IEnumerable<Suggestion> TryProvideSuggestions(BeginTagToken tagToken, Func<ControlInnerElementsReader> readerFactory)
        {
            var reader = readerFactory();

            if (tagToken.TagName.StartsWith("asp:"))
            {
                yield return new Suggestion()
                {
                    Description = "Change to <code>&lt;dot:GridViewTemplateColumn&gt;</code>",
                    Fixes = new FixAction[]
                    {
                        new RenameElementFix(tagToken, "dot:GridViewTemplateColumn", reader.EndTag)
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

            if (reader.Elements.TryGetValue("ItemTemplate", out var itemTemplate))
            {
                yield return new Suggestion()
                {
                    Description = "Rename <code>ItemTemplate</code> to <code>ContentTemplate</code>",
                    Fixes = new FixAction[]
                    {
                        new RenameElementFix((BeginTagToken)itemTemplate[0], "ContentTemplate", (EndTagToken)itemTemplate[^1])
                    }
                };
            }

            if (reader.Elements.TryGetValue("EditItemTemplate", out var editTemplate))
            {
                yield return new Suggestion()
                {
                    Description = "Rename <code>EditItemTemplate</code> to <code>EditTemplate</code>",
                    Fixes = new FixAction[]
                    {
                        new RenameElementFix((BeginTagToken)editTemplate[0], "EditTemplate", (EndTagToken)editTemplate[^1])
                    }
                };
            }

            if (reader.Elements.TryGetValue("InsertItemTemplate", out var insertTemplate))
            {
                yield return new Suggestion()
                {
                    Description = "Rename <code>InsertItemTemplate</code> to <code>InsertTemplate</code>",
                    Fixes = new FixAction[]
                    {
                        new RenameElementFix((BeginTagToken)insertTemplate[0], "InsertTemplate", (EndTagToken)insertTemplate[^1])
                    }
                };
            }
        }
    }
}