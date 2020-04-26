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
        public override string[] MatchedTagNames => new[] { "asp:Button", "asp:LinkButton" };

        protected override IEnumerable<Suggestion> TryProvideSuggestions(BeginTagToken tagToken, Func<ControlInnerElementsReader> readerFactory)
        {
            var reader = readerFactory();

            var id = tagToken.FindOrGenerateId();

            var commandName = "On" + id + "Click";
            yield return new Suggestion()
            {
                Description = $"Change to <code>&lt;dot:{tagToken.TagNameWithoutPrefix}&gt;</code>",
                Fixes = new FixAction[]
                {
                    new RenameElementFix(tagToken, "dot:" + tagToken.TagNameWithoutPrefix, reader.EndTag),
                    new SetAttributeCommandBindingFix(tagToken, "Click", commandName), 
                    new AddViewModelMethodFix(commandName, "void", $"// handle click on {id} button") 
                }
            };

            var onclick = tagToken.FindAttribute("OnClick");
            if (onclick != null)
            {
                yield return new Suggestion()
                {
                    Description = $"Remove <code>OnClick</code> attribute",
                    Fixes = new FixAction[]
                    {
                        new RemoveAttributeFix(tagToken, onclick)
                    }
                };
            }
        }
    }
}