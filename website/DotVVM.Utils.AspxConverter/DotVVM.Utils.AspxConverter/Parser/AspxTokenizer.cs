using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Transactions;
using System.Web;
using DotVVM.Utils.AspxConverter.Parser.Tokens;

namespace DotVVM.Utils.AspxConverter.Parser
{
    public class AspxTokenizer
    {
        private int current;
        private string markup;


        public IEnumerable<AspxToken> Tokenize(string markup)
        {
            this.markup = markup;

            current = 0;
            while (true)
            {
                var literal = AcceptUntil("<");
                if (literal == null)
                {
                    break;
                }

                if (literal.Length > 0)
                {
                    yield return new LiteralToken(current, literal);
                }

                if (ContinuesWith("<"))
                {
                    if (ContinuesWith("<%@"))
                    {
                        // directive
                        var text = AcceptThrough("%>");
                        yield return new DirectiveToken(current, text[..2], text[2..3], text[3..^2], text[^2..]);

                    }
                    else if (ContinuesWith("<%$"))
                    {
                        // builder
                        var text = AcceptThrough("%>");
                        yield return new ExpressionBuilderToken(current, text[..2], text[2..3], text[3..^2], text[^2..]);
                    }
                    else if (ContinuesWith("<%--"))
                    {
                        // comment
                        var text = AcceptThrough("--%>");
                        yield return new CommentToken(current, text[..2], text[2..^2], text[^2..]);
                    }
                    else if (ContinuesWith("<%#:"))
                    {
                        // binding
                        var text = AcceptThrough("%>");
                        yield return new BindingBlockToken(current, text[..2], text[2..4], text[4..^2], text[^2..]);
                    }
                    else if (ContinuesWith("<%#"))
                    {
                        // binding
                        var text = AcceptThrough("%>");
                        yield return new BindingBlockToken(current, text[..2], text[2..3], text[3..^2], text[^2..]);
                    }
                    else if (ContinuesWith("<%"))
                    {
                        // code block
                        var text = AcceptThrough("%>");
                        yield return new CodeBlockToken(current, text[..2], text[2..^2], text[^2..]);
                    }
                    else
                    {
                        // regular tag
                        var tag = ParseTag();
                        yield return tag; 
                        
                        // the script and style tags should not parse the inner content
                        if (string.Equals(tag.TagName, "script", StringComparison.CurrentCultureIgnoreCase)
                            || string.Equals(tag.TagName, "style", StringComparison.CurrentCultureIgnoreCase))
                        {
                            var scriptStart = current;

                            var endRegex = new Regex(@"(\<\s*/\s*)" + tag.TagName + @"(\s*\>)", RegexOptions.IgnoreCase);
                            var end = endRegex.Match(markup, current);
                            if (!end.Success)
                            {
                                ReportError($"Unclosed {tag.TagName} found!");
                            }
                            yield return new LiteralToken(scriptStart, markup[scriptStart..end.Index]);
                            yield return new EndTagToken(current + end.Length, end.Groups[1].Value, tag.TagName, end.Groups[2].Value);
                            current = end.Index + end.Length;
                        }
                    }
                }
            }

            if (current < markup.Length)
            {
                yield return new LiteralToken(current, markup[current..markup.Length]);
            }
        }

        private TagToken ParseTag()
        {
            var tagStart = current;
            AcceptChar();
            AcceptWhitespace();

            int? tagSlash = null;
            if (ContinuesWith("/"))
            {
                // it is the end tag
                tagSlash = current;
                AcceptChar();
                AcceptWhitespace();
            }

            // read tag name
            var beforeTagName = markup[tagStart..current];
            var tagName = AcceptNonWhitespace('<', '=', '/', '>', '\'', '"');
            var tagNameEnd = current;
            AcceptWhitespace();

            if (tagSlash != null)
            {
                if (!ContinuesWith(">"))
                {
                    ReportError("End tags with attributes are not supported!");
                }

                AcceptChar();
                var afterTagName = markup[tagNameEnd..current];
                return new EndTagToken(tagStart, beforeTagName, tagName, afterTagName);
            }

            int? tagSelfClosingSlash = null;
            var attributes = new List<AttributeToken>();
            while (true)
            {
                if (ContinuesWith("/"))
                {
                    // self-closing tag
                    tagSelfClosingSlash = current;
                    AcceptChar();
                    AcceptWhitespace();

                    if (!ContinuesWith(">"))
                    {
                        ReportError("'>' must follow the '/' in self-closing tag!");
                    }
                }
                else if (ContinuesWith(">"))
                {
                    // end of the tag
                    AcceptChar();

                    var afterAttributesText = markup[tagNameEnd..current];
                    return new BeginTagToken(tagStart, beforeTagName, tagName, tagSelfClosingSlash != null, attributes, afterAttributesText);
                }
                else if (ContinuesWith("<"))
                {
                    // unexpected end of the tag
                    var afterAttributesText = markup[tagNameEnd..current];
                    return new BeginTagToken(tagStart, beforeTagName, tagName, tagSelfClosingSlash != null, attributes, afterAttributesText);
                }
                else
                {
                    // attribute
                    var attributeStart = current;
                    var beforeAttributeName = markup[tagNameEnd..current];
                    var name = AcceptNonWhitespace('<', '=', '/', '>', '\'', '"');
                    if (name.Length == 0)
                    {
                        // unexpected end of the tag
                        var afterAttributesText = markup[tagNameEnd..current];
                        return new BeginTagToken(tagStart, beforeTagName, tagName, tagSelfClosingSlash != null, attributes, afterAttributesText);
                    }

                    tagNameEnd = current;
                    AcceptWhitespace();

                    if (ContinuesWith("="))
                    {
                        AcceptChar();
                        AcceptWhitespace();
                        var beforeValue = markup[tagNameEnd..current];

                        if (ContinuesWith("\"") || ContinuesWith("'"))
                        {
                            // quoted attribute
                            var quotesStart = current;
                            var quoteChar = AcceptChar();

                            AcceptUntil(quoteChar);
                            AcceptChar();
                            var attributeValueText = markup[quotesStart..current];
                            var value = new AttributeQuotedValueToken(quotesStart, quoteChar, attributeValueText[1..^1]);
                            attributes.Add(new AttributeToken(attributeStart, beforeAttributeName, name, beforeValue, value));
                        }
                        else
                        {
                            // unquoted attribute
                            var valueStart = current;
                            var text = AcceptNonWhitespace('/', '>');

                            var value = new AttributeUnquotedValueToken(valueStart, text);
                            attributes.Add(new AttributeToken(attributeStart, beforeAttributeName, name, beforeValue, value));
                        }
                        tagNameEnd = current;

                        AcceptWhitespace();
                    }
                    else
                    {
                        attributes.Add(new AttributeToken(attributeStart, beforeAttributeName, name, "", new AttributeEmptyValueToken(current)));
                    }

                }
            }
        }

        private void ReportError(string message)
        {
            throw new Exception(message);
        }

        private string AcceptUntil(string match)
        {
            var pos = markup.IndexOf(match, current);
            if (pos >= 0)
            {
                var text = markup[current..pos];
                current += text.Length;
                return text;
            }

            return null;
        }

        private string AcceptThrough(string match)
        {
            var pos = markup.IndexOf(match, current);
            if (pos >= 0)
            {
                pos += match.Length;
                var text = markup[current..pos];
                current += text.Length;
                return text;
            }

            return null;
        }

        private string AcceptChar()
        {
            var text = markup[current].ToString();
            current++;
            return text;
        }

        private string AcceptWhitespace()
        {
            var pos = current;
            while (pos < markup.Length && char.IsWhiteSpace(markup[pos]))
            {
                pos++;
            }

            var text = markup[current..pos];
            current = pos;
            return text;
        }

        private string AcceptNonWhitespace(params char[] stopChars)
        {
            var pos = current;
            while (pos < markup.Length && !char.IsWhiteSpace(markup[pos]) && !stopChars.Contains(markup[pos]))
            {
                pos++;
            }

            var text = markup[current..pos];
            current = pos;
            return text;
        }

        private bool ContinuesWith(string nextChars)
        {
            return (current + nextChars.Length <= markup.Length)
                   && markup.Substring(current, nextChars.Length) == nextChars;
        }

    }
}