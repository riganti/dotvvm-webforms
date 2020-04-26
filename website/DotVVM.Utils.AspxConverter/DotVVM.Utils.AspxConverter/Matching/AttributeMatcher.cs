using System;
using System.Collections.Generic;
using System.Linq;
using DotVVM.Utils.AspxConverter.Parser.Tokens;

namespace DotVVM.Utils.AspxConverter.Matching
{
    public abstract class AttributeMatcher : IMatcher
    {
        public abstract string MatchedAttributeName { get; }

        public List<SuggestionInstance> TryConsume(List<AspxToken> tokens, int index)
        {
            var token = tokens[index];
            if (token is BeginTagToken beginTagToken)
            {
                var attributeIndex = beginTagToken.Attributes.FindIndex(a => string.Equals(a.Name, MatchedAttributeName));
                if (attributeIndex >= 0)
                {
                    var suggestions = TryProvideSuggestions(beginTagToken, beginTagToken.Attributes[attributeIndex]).ToList();
                    if (suggestions.Any())
                    {
                        return suggestions
                            .Select((s, i) => new SuggestionInstance()
                            {
                                Suggestion = s,
                                TagIndex = index,
                                AttributeIndex = attributeIndex,
                                Index = i
                            })
                            .ToList();
                    }
                }
            }

            return null;
        }

        protected abstract IEnumerable<Suggestion> TryProvideSuggestions(BeginTagToken tagToken, AttributeToken attributeToken);

    }
}