using DotVVM.Utils.AspxConverter.Matching.Fixes;
using DotVVM.Utils.AspxConverter.Parser.Tokens;
using DotVVM.Utils.AspxConverter.Parser;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DotVVM.Utils.AspxConverter.Matching.Matchers
{
    public class LoginViewMatcher : ControlMatcher
    {
        public override string[] MatchedTagNames => new[] { "asp:LoginView", "dot:AuthenticatedView" };

        protected override IEnumerable<Suggestion> TryProvideSuggestions(BeginTagToken tagToken, Func<ControlInnerElementsReader> readerFactory)
        {
            var reader = readerFactory();

            if (tagToken.TagName.StartsWith("asp:"))
            {
                yield return new Suggestion()
                {
                    Description = "Change to <code>&lt;dot:AuthenticatedView&gt;</code>",
                    Fixes = new FixAction[]
                    {
                        new RenameElementFix(tagToken, "dot:AuthenticatedView", reader.EndTag)
                    }
                };
            }

            if (reader.Elements.TryGetValue("LoggedInTemplate", out var loggedInTemplate))
            {
                yield return new Suggestion()
                {
                    Description = "Rename <code>LoggedInTemplate</code> to <code>AuthenticatedTemplate</code>",
                    Fixes = new FixAction[]
                    {
                        new RenameElementFix((BeginTagToken)loggedInTemplate[0], "AuthenticatedTemplate", (EndTagToken)loggedInTemplate[^1])
                    }
                };
            }

            if (reader.Elements.TryGetValue("AnonymousTemplate", out var anonymousTemplate))
            {
                yield return new Suggestion()
                {
                    Description = "Rename <code>AnonymousTemplate</code> to <code>NotAuthenticatedTemplate</code>",
                    Fixes = new FixAction[]
                    {
                        new RenameElementFix((BeginTagToken)anonymousTemplate[0], "NotAuthenticatedTemplate", (EndTagToken)anonymousTemplate[^1])
                    }
                };
            }
        }
    }
}
