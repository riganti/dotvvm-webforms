using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotVVM.Utils.AspxConverter.Matching;
using DotVVM.Utils.AspxConverter.Parser;
using DotVVM.Utils.AspxConverter.Parser.Tokens;

namespace DotVVM.Utils.AspxConverter.Workspace
{
    public class ConverterWorkspace
    {
        private bool isInitialized;
        private Dictionary<AspxToken, List<SuggestionInstance>> suggestionsMap;

        public string DisplayHtml { get; private set; }

        public List<SuggestionData> Suggestions { get; private set; }

        public List<SuggestionGroupData> SuggestionGroups => GroupSuggestions();

        public List<AspxToken> Tokens { get; private set; }

        public List<CsharpFileBuilder> CsharpFiles { get; private set; }

        public void Initialize(string markup, List<CsharpFileBuilder> currentCsharpFiles = null)
        {
            if (isInitialized)
            {
                throw new InvalidOperationException("The workspace has already been initialized.");
            }

            TokenizeAndGetSuggestions(markup);
            CsharpFiles = currentCsharpFiles ?? new List<CsharpFileBuilder>();

            isInitialized = true;
        }

        private void TokenizeAndGetSuggestions(string markup)
        {
            var generator = new DisplayHtmlGenerator();
            var matcherService = new MatcherService();

            var tokenizer = new AspxTokenizer();
            Tokens = tokenizer.Tokenize(markup).ToList();
            suggestionsMap = matcherService.GetSuggestions(Tokens);

            (DisplayHtml, Suggestions) = generator.GenerateDisplayHtml(Tokens, suggestionsMap);
        }

        public void ApplyFixes(string[] uniqueIds)
        {
            if (!isInitialized)
            {
                throw new InvalidOperationException("The workspace must be initialized first.");
            }

            // find suggestions
            var suggestions = suggestionsMap
                .SelectMany(e => e.Value.Select(s => new
                    {
                        Token = e.Key,
                        Suggestion = s
                    })
                    .Where(s => uniqueIds.Contains(s.Suggestion.UniqueId))
                )
                .ToList();
            if (suggestions.Count != uniqueIds.Length)
            {
                throw new InvalidOperationException("The workspace is outdated. Please recalculate the suggestions.");
            }

            // apply fixes
            foreach (var suggestion in suggestions)
            {
                foreach (var fix in suggestion.Suggestion.Suggestion.Fixes)
                {
                    var context = new WorkspaceFixContext(suggestion.Suggestion.Suggestion, fix, Tokens, CsharpFiles);
                    try
                    {
                        fix.Apply(context);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"Couldn't apply fix {fix.GetType()}. " + ex.Message, ex);
                    }
                }
            }

            // generate new markup
            var newMarkup = GetMarkup();
            TokenizeAndGetSuggestions(newMarkup);
        }

        public string GetMarkup()
        {
            var sb = new StringBuilder();
            foreach (var token in Tokens)
            {
                sb.Append(token.Fragment);
            }
            return sb.ToString();
        }

        private List<SuggestionGroupData> GroupSuggestions()
        {
            return Suggestions
                .GroupBy(s => new { s.Description, s.HelpUrl })
                .Select(s => new SuggestionGroupData()
                {
                    Description = s.Key.Description,
                    HelpUrl = s.Key.HelpUrl,
                    Suggestions = s.Select(i => new SuggestionGroupItemData()
                        {
                            SpanIndex = i.SpanIndex,
                            UniqueId = i.UniqueId
                        })
                        .ToList()
                })
                .ToList();
        }
    }
}