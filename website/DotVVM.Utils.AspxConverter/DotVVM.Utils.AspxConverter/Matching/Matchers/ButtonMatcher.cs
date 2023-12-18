using System;
using System.Collections.Generic;
using System.Linq;
using DotVVM.Utils.AspxConverter.Matching.Fixes;
using DotVVM.Utils.AspxConverter.Parser;
using DotVVM.Utils.AspxConverter.Parser.Tokens;

namespace DotVVM.Utils.AspxConverter.Matching.Matchers
{
    public class ButtonMatcher : ControlMatcher
    {
        public override string[] MatchedTagNames => new[] { "asp:Button", "asp:LinkButton", "dot:Button", "dot:LinkButton" };

        protected override IEnumerable<Suggestion> TryProvideSuggestions(BeginTagToken tagToken, Func<ControlInnerElementsReader> readerFactory)
        {
            var reader = readerFactory();

            if (tagToken.TagName.StartsWith("asp:"))
            {
                yield return new Suggestion()
                {
                    Description = $"Change to <code>&lt;dot:{tagToken.TagNameWithoutPrefix}&gt;</code>",
                    Fixes = new FixAction[]
                    {
                        new RenameElementFix(tagToken, "dot:" + tagToken.TagNameWithoutPrefix, reader.EndTag)
                    }
                };
            }

            var onclick = tagToken.FindAttribute("OnClick");
            if (onclick != null)
            {
                var id = tagToken.FindOrGenerateId();
                var commandName = "On" + id + "Click";

                yield return new Suggestion()
                {
                    Description = $"Remove <code>OnClick</code> attribute",
                    Fixes = new FixAction[]
                    {
                        new RemoveAttributeFix(tagToken, onclick),
                        new SetAttributeCommandBindingFix(tagToken, "Click", commandName),
                        new AddViewModelMethodFix(commandName, "void", $"// handle click on {id} button")
                    }
                };
            }
        }
    }
}