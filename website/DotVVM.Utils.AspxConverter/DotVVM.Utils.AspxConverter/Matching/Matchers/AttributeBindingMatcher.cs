using System;
using System.Collections.Generic;
using DotVVM.Utils.AspxConverter.Matching.Fixes;
using DotVVM.Utils.AspxConverter.Matching.Utils;
using DotVVM.Utils.AspxConverter.Parser.Tokens;

namespace DotVVM.Utils.AspxConverter.Matching.Matchers
{
    public class AttributeBindingMatcher : IMatcher
    {
        public List<SuggestionInstance> TryConsume(List<AspxToken> tokens, int index)
        {
            var token = tokens[index];
            if (token is BeginTagToken beginTagToken)
            {
                var suggestions = new List<SuggestionInstance>();
                for (var attributeIndex = 0; attributeIndex < beginTagToken.Attributes.Count; attributeIndex++)
                {
                    var attribute = beginTagToken.Attributes[attributeIndex];
                    if (BindingParser.TryParseBinding(attribute.GetValue(), out var binding))
                    {
                        binding = BindingParser.TranslateBinding(binding);
                        suggestions.Add(new SuggestionInstance()
                        {
                            Suggestion = new Suggestion() {
                                Description = "Change to data-bind expression",
                                Fixes = new FixAction[]
                                {
                                    new SetAttributeValueFix(beginTagToken, attribute.Name, $"{{value: {binding}}}")
                                }
                            },
                            TagIndex = index,
                            AttributeIndex = attributeIndex,
                            Index = suggestions.Count
                        });
                    }
                }

                return suggestions;
            }

            return null;
        }
    }
}
