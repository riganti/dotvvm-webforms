using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using DotVVM.Utils.AspxConverter.Parser;
using DotVVM.Utils.AspxConverter.Parser.Tokens;

namespace DotVVM.Utils.AspxConverter.Matching
{
    public abstract class ControlMatcher : IMatcher
    {
        public abstract string[] MatchedTagNames { get; }

        public List<SuggestionInstance> TryConsume(List<AspxToken> tokens, int index)
        {
            var token = tokens[index];
            if (token is BeginTagToken beginTagToken)
            {
                if (MatchedTagNames.Any(n => string.Equals(beginTagToken.TagName, n, StringComparison.CurrentCultureIgnoreCase)))
                {
                    var suggestions = TryProvideSuggestions(beginTagToken, () => new ControlInnerElementsReader(tokens, index)).ToList();
                    if (suggestions.Any())
                    {
                        return suggestions
                            .Select((s, i) => new SuggestionInstance()
                            {
                                Suggestion = s,
                                TagIndex = index,
                                Index = i
                            })
                            .ToList();
                    }
                }
            }

            return null;
        }

        protected abstract IEnumerable<Suggestion> TryProvideSuggestions(BeginTagToken tagToken, Func<ControlInnerElementsReader> readerFactory);

    }
}