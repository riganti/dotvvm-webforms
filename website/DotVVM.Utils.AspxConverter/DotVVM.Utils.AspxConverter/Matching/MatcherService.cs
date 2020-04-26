using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using DotVVM.Utils.AspxConverter.Parser.Tokens;

namespace DotVVM.Utils.AspxConverter.Matching
{
    public class MatcherService
    {
        private readonly List<IMatcher> matchers;

        public MatcherService()
        {
            matchers = typeof(IMatcher).Assembly
                .GetExportedTypes()
                .Where(t => !t.IsAbstract && t.IsClass)
                .Where(t => typeof(IMatcher).IsAssignableFrom(t))
                .Select(Activator.CreateInstance)
                .OfType<IMatcher>()
                .ToList();
        }

        public Dictionary<AspxToken, List<SuggestionInstance>> GetSuggestions(List<AspxToken> tokens)
        {
            var results = new Dictionary<AspxToken, List<SuggestionInstance>>();
            for (var i = 0; i < tokens.Count; i++)
            {
                var tokenSuggestions = new List<SuggestionInstance>();

                foreach (var matcher in matchers)
                {
                    if (matcher.TryConsume(tokens, i) is {} suggestions)
                    {
                        tokenSuggestions.AddRange(suggestions);
                    }
                }

                if (tokenSuggestions.Any())
                {
                    results[tokens[i]] = tokenSuggestions;
                }
            }

            return results;
        }

    }
}
