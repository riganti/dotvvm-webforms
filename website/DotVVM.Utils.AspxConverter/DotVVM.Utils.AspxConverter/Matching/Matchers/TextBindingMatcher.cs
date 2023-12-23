using DotVVM.Utils.AspxConverter.Matching.Fixes;
using DotVVM.Utils.AspxConverter.Matching.Utils;
using DotVVM.Utils.AspxConverter.Parser.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotVVM.Utils.AspxConverter.Matching.Matchers
{
    public class TextBindingMatcher : IMatcher
    {
        public List<SuggestionInstance> TryConsume(List<AspxToken> tokens, int index)
        {
            var token = tokens[index];
            if (token is BindingBlockToken bindingToken)
            {
                var binding = BindingParser.TranslateBinding(bindingToken.Content);
                var suggestions = new List<SuggestionInstance>();
                suggestions.Add(new SuggestionInstance()
                {
                    Suggestion = new Suggestion()
                    {
                        Description = "Change to data-bind expression",
                        Fixes = new FixAction[]
                        {
                            new ReplaceTokenFix(bindingToken, new LiteralToken(0, $"{{{{value: {binding}}}}}"))
                        }
                    },
                    Index = suggestions.Count
                });

                return suggestions;
            }

            return null;
        }
    }
}
