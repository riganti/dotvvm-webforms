using System;
using System.Collections.Generic;
using System.Text;
using DotVVM.Utils.AspxConverter.Matching.Fixes;
using DotVVM.Utils.AspxConverter.Parser;
using DotVVM.Utils.AspxConverter.Parser.Tokens;

namespace DotVVM.Utils.AspxConverter.Matching.Matchers
{
    public class TextBoxMatcher : ControlMatcher
    {
        public override string[] MatchedTagNames => new[] { "asp:TextBox", "dot:TextBox" }; 

        protected override IEnumerable<Suggestion> TryProvideSuggestions(BeginTagToken tagToken, Func<ControlInnerElementsReader> readerFactory)
        {
            var reader = readerFactory();

            var id = tagToken.FindOrGenerateId();

            if (tagToken.TagName.StartsWith("asp:"))
            {
                yield return new Suggestion()
                {
                    Description = "Change to <code>&lt;dot:TextBox&gt;</code> and generate a viewmodel property",
                    Fixes = new FixAction[]
                    {
                        new RenameElementFix(tagToken, "dot:TextBox", reader.EndTag),
                        new SetAttributeValueBindingFix(tagToken, "Text", id),
                        new AddViewModelPropertyFix(id, "string")
                    }
                };
            }

            var autoPostBack = tagToken.FindAttribute("AutoPostBack");
            if (autoPostBack != null)
            {
                var changedEventName = "On" + id + "Changed";

                yield return new Suggestion()
                {
                    Description = "Add a viewmodel command to handle the text change",
                    Fixes = new FixAction[]
                    {
                        new RemoveAttributeFix(tagToken, autoPostBack),
                        new SetAttributeCommandBindingFix(tagToken, "Changed", changedEventName),
                        new AddViewModelMethodFix(changedEventName, "void", $"// handle {id} property change")
                    }
                };
            }
        }
    }
}
