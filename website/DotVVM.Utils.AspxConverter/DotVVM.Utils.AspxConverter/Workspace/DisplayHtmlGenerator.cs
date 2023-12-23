using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using DotVVM.Utils.AspxConverter.Matching;
using DotVVM.Utils.AspxConverter.Parser.Tokens;

namespace DotVVM.Utils.AspxConverter.Workspace
{
    public class DisplayHtmlGenerator
    {

        public (string html, List<SuggestionData> suggestions) GenerateDisplayHtml(List<AspxToken> tokens, Dictionary<AspxToken, List<SuggestionInstance>> suggestionsMap)
        {
            var sb = new StringBuilder();
            var data = new List<SuggestionData>();

            foreach (var token in tokens)
            {
                if (suggestionsMap.TryGetValue(token, out var suggestions))
                {
                    foreach (var suggestion in suggestions)
                    {
                        sb.Append($"<span data-index='{suggestion.SpanIndex}'>");
                    }

                    data.AddRange(suggestions.Select(s => new SuggestionData()
                    {
                        Description = s.Suggestion.Description,
                        HelpUrl = s.Suggestion.HelpUrl,
                        SpanIndex = s.SpanIndex,
                        UniqueId = s.UniqueId
                    }));
                }

                if (token is LiteralToken literal)
                {
                    AppendLiteral(literal.Fragment);
                }
                else if (token is DirectiveToken || token is BindingBlockToken || token is ExpressionBuilderToken)
                {
                    sb.Append($"<b class='y'>{WebUtility.HtmlEncode(token.Fragment[..2])}</b>");
                    sb.Append($"<b class='b'>{WebUtility.HtmlEncode(token.Fragment[2].ToString())}</b>");
                    sb.Append($"<b class='m'>{WebUtility.HtmlEncode(token.Fragment[3..^2])}</b>");
                    sb.Append($"<b class='y'>{WebUtility.HtmlEncode(token.Fragment[^2..])}</b>");
                }
                else if (token is CodeBlockToken)
                {
                    sb.Append($"<b class='y'>{WebUtility.HtmlEncode(token.Fragment[..2])}</b>");
                    sb.Append($"<b class='m'>{WebUtility.HtmlEncode(token.Fragment[2..^2])}</b>");
                    sb.Append($"<b class='y'>{WebUtility.HtmlEncode(token.Fragment[^2..])}</b>");
                }
                else if (token is CommentToken)
                {
                    sb.Append($"<b class='g'>{WebUtility.HtmlEncode(token.Fragment)}</b>");
                }
                else if (token is BeginTagToken beginTag)
                {
                    sb.Append($"<b class='b'>{WebUtility.HtmlEncode(beginTag.BeforeNameFragment)}</b>");
                    sb.Append($"<b class='m'>{WebUtility.HtmlEncode(beginTag.TagName)}</b>");

                    foreach (var attribute in beginTag.Attributes)
                    {
                        if (suggestionsMap.TryGetValue(attribute, out var attributeSuggestions))
                        {
                            foreach (var attributeSuggestion in attributeSuggestions)
                            {
                                sb.Append($"<span data-index='{attributeSuggestion.SpanIndex}'>");
                            }

                            data.AddRange(attributeSuggestions.Select(s => new SuggestionData()
                            {
                                Description = s.Suggestion.Description,
                                HelpUrl = s.Suggestion.HelpUrl,
                                SpanIndex = s.SpanIndex,
                                UniqueId = s.UniqueId
                            }));
                        }

                        sb.Append($"<b class='r'>{WebUtility.HtmlEncode(attribute.BeforeNameFragment)}</b>");
                        sb.Append($"<b class='r'>{WebUtility.HtmlEncode(attribute.Name)}</b>");
                        sb.Append($"<b class='b'>{WebUtility.HtmlEncode(attribute.BeforeValueFragment)}</b>");
                        if (attribute.Value is AttributeQuotedValueToken quotedAttribute)
                        {
                            sb.Append($"<b class='b'>{WebUtility.HtmlEncode(quotedAttribute.QuoteChar)}</b>");
                            AppendAttributeValue(quotedAttribute.Value);
                            sb.Append($"<b class='b'>{WebUtility.HtmlEncode(quotedAttribute.QuoteChar)}</b>");
                        }
                        else
                        {
                            AppendAttributeValue(attribute.Value.Fragment);
                        }

                        if (attributeSuggestions != null)
                        {
                            foreach (var attributeSuggestion in attributeSuggestions)
                            {
                                sb.Append($"</span>");
                            }
                        }
                    }
                    sb.Append($"<b class='b'>{WebUtility.HtmlEncode(beginTag.AfterAttributesFragment)}</b>");
                }
                else if (token is EndTagToken endTag)
                {
                    sb.Append($"<b class='b'>{WebUtility.HtmlEncode(endTag.BeforeNameFragment)}</b>");
                    sb.Append($"<b class='m'>{WebUtility.HtmlEncode(endTag.TagName)}</b>");
                    sb.Append($"<b class='b'>{WebUtility.HtmlEncode(endTag.AfterNameFragment)}</b>");
                }

                if (suggestions != null)
                {
                    foreach (var suggestion in suggestions)
                    {
                        sb.Append($"</span>");
                    }
                }
            }

            return (sb.ToString(), data);

            void AppendAttributeValue(string value)
            {
                if ((value.StartsWith("<%$") || value.StartsWith("<%#")) && value.EndsWith("%>"))
                {
                    sb.Append($"<b class='y'>{WebUtility.HtmlEncode(value[..3])}</b>");
                    sb.Append($"<b class='b'>{WebUtility.HtmlEncode(value[3..^2])}</b>");
                    sb.Append($"<b class='y'>{WebUtility.HtmlEncode(value[^2..])}</b>");
                }
                else if ((value.StartsWith("<%$:") || value.StartsWith("<%#:")) && value.EndsWith("%>"))
                {
                    sb.Append($"<b class='y'>{WebUtility.HtmlEncode(value[..4])}</b>");
                    sb.Append($"<b class='b'>{WebUtility.HtmlEncode(value[4..^2])}</b>");
                    sb.Append($"<b class='y'>{WebUtility.HtmlEncode(value[^2..])}</b>");
                }
                else if (value.StartsWith("{{") && value.EndsWith("}}"))
                {
                    sb.Append($"<b class='y'>{WebUtility.HtmlEncode(value[..2])}</b>");
                    sb.Append($"<b class='b'>{WebUtility.HtmlEncode(value[2..^2])}</b>");
                    sb.Append($"<b class='y'>{WebUtility.HtmlEncode(value[^2..])}</b>");
                }
                else if (value.StartsWith("{") && value.EndsWith("}"))
                {
                    sb.Append($"<b class='y'>{WebUtility.HtmlEncode(value[..1])}</b>");
                    sb.Append($"<b class='b'>{WebUtility.HtmlEncode(value[1..^1])}</b>");
                    sb.Append($"<b class='y'>{WebUtility.HtmlEncode(value[^1..])}</b>");
                }
                else
                {
                    sb.Append($"<b class='b'>{WebUtility.HtmlEncode(value)}</b>");
                }
            }

            void AppendLiteral(string value)
            {
                var current = 0;
                var next = value.IndexOf("{{", current);
                while (next >= 0)
                {
                    if (next > current)
                    {
                        sb.Append($"<b class='b'>{WebUtility.HtmlEncode(value[current..next])}</b>");
                    }
                    sb.Append("<b class='y'>{{</b>");
                    current = next + 2;

                    next = value.IndexOf("}}", current);
                    if (next < 0)
                    {
                        break;
                    }
                    sb.Append($"<b class='b'>{WebUtility.HtmlEncode(value[current..next])}</b>");
                    sb.Append("<b class='y'>}}</b>");
                    current = next + 2;

                    next = value.IndexOf("{{", current);
                }

                if (current < value.Length)
                {
                    sb.Append($"<b class='b'>{WebUtility.HtmlEncode(value[current..])}</b>");
                }
            }
        }

    }
}