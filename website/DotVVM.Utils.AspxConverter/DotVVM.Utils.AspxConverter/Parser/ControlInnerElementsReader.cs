using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotVVM.Utils.AspxConverter.Parser.Tokens;

namespace DotVVM.Utils.AspxConverter.Parser
{
    public class ControlInnerElementsReader
    {
        private readonly List<AspxToken> tokens;
        private int index;

        public BeginTagToken BeginTag { get; }

        public EndTagToken EndTag { get; private set; }

        public IReadOnlyDictionary<string, IList<AspxToken>> Elements { get; }


        public ControlInnerElementsReader(List<AspxToken> tokens, int index)
        {
            this.tokens = tokens;
            this.index = index;
            BeginTag = (BeginTagToken)tokens[index];

            Elements = ReadElements();
        }

        private Dictionary<string, IList<AspxToken>> ReadElements()
        {
            var elements = new Dictionary<string, IList<AspxToken>>(StringComparer.CurrentCultureIgnoreCase);

            if (!BeginTag.IsSelfClosing)
            {
                var token = GetNextToken();
                while (token != null)
                {
                    if (token is BeginTagToken beginTag)
                    {
                        elements[beginTag.TagName] = ReadTokens(beginTag);
                    }
                    else if (token is EndTagToken endTag)
                    {
                        if (string.Equals(endTag.TagName, BeginTag.TagName, StringComparison.CurrentCultureIgnoreCase))
                        {
                            EndTag = endTag;
                            break;
                        }
                    }

                    token = GetNextToken();
                }
            }

            return elements;
        }
        private IList<AspxToken> ReadTokens(BeginTagToken beginTag)
        {
            var elementTokens = new List<AspxToken>
            {
                beginTag
            };

            if (!beginTag.IsSelfClosing)
            {
                // read all tags until the end tag
                var openTags = new Stack<string>();
                openTags.Push(beginTag.TagName);

                var token = GetNextToken();
                while (token != null)
                {
                    elementTokens.Add(token);

                    if (token is BeginTagToken b && !b.IsSelfClosing)
                    {
                        openTags.Push(b.TagName);
                    }
                    else if (token is EndTagToken e)
                    {
                        if (openTags.Contains(e.TagName, StringComparer.CurrentCultureIgnoreCase))
                        {
                            // ignore closing tags that are not on the stack
                            var lastTag = openTags.Pop();
                            while (!string.Equals(e.TagName, lastTag, StringComparison.CurrentCultureIgnoreCase) && openTags.Any())
                            {
                                openTags.Pop();
                            }
                        }
                        
                        if (!openTags.Any())
                        {
                            break;
                        }
                    }

                    token = GetNextToken();
                }
            }

            return elementTokens;
        }

        private AspxToken GetNextToken()
        {
            index++;
            if (index < tokens.Count)
            {
                return tokens[index];
            }
            return null;
        }
    }
}
