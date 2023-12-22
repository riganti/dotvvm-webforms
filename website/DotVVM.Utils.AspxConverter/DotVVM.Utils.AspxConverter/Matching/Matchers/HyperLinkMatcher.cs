using System;
using System.Collections.Generic;
using System.Linq;
using DotVVM.Utils.AspxConverter.Matching.Fixes;
using DotVVM.Utils.AspxConverter.Matching.Utils;
using DotVVM.Utils.AspxConverter.Parser;
using DotVVM.Utils.AspxConverter.Parser.Tokens;

namespace DotVVM.Utils.AspxConverter.Matching.Matchers
{
    public class HyperLinkMatcher : ControlMatcher
    {
        public override string[] MatchedTagNames => new[] { "asp:HyperLink", "a", "webforms:HybridRouteLink" };

        protected override IEnumerable<Suggestion> TryProvideSuggestions(BeginTagToken tagToken, Func<ControlInnerElementsReader> readerFactory)
        {
            var reader = readerFactory();
            
            if (tagToken.FindAttribute("NavigateUrl") is { } navigateUrl)
            {
                // convert to <webforms:HybridRouteLink>
                if (RouteUrlExpressionParser.TryParse(navigateUrl.GetValue(), out var routeName, out var routeValues))
                {
                    if (!tagToken.TagName.StartsWith("webforms:"))
                    {
                        yield return new Suggestion()
                        {
                            Description = "Change to <code>&lt;webforms:HybridRouteLink&gt;</code>",
                            Fixes = new FixAction[]
                            {
                                new RenameElementFix(tagToken, "webforms:HybridRouteLink", reader.EndTag)
                            }
                        };
                    }

                    yield return new Suggestion()
                    {
                        Description = "Change <code>NavigateUrl</code> to <code>RouteName</code>",
                        Fixes = new FixAction[]
                            {
                                new RemoveAttributeFix(tagToken, navigateUrl),
                                new SetAttributeValueFix(tagToken, "RouteName", routeName)
                            }
                            .Concat(
                                routeValues.Select(v => new SetAttributeValueFix(tagToken, "Param-" + v.Key, v.Value))
                            )
                            .ToArray()
                    };
                }
                else
                {
                    // convert to <a>
                    if (tagToken.TagName.StartsWith("asp:"))
                    {
                        yield return new Suggestion()
                        {
                            Description = "Change to <code>&lt;a&gt;</code>",
                            Fixes = new FixAction[]
                            {
                                new RenameElementFix(tagToken, "a", reader.EndTag)
                            }
                        };
                    }

                    if (tagToken.FindAttribute("Text") is { } textAttribute
                        && reader.HasEmptyContent)
                    {
                        yield return new Suggestion()
                        {
                            Description = "Move <code>Text</code> attribute inside",
                            Fixes = new FixAction[]
                            {
                                new RemoveAttributeFix(tagToken, textAttribute),
                                new AddInnerContentFix(tagToken, reader.EndTag, textAttribute.GetValue())
                            }
                        };
                    }
                    
                    yield return new Suggestion()
                    {
                        Description = "Change <code>NavigateUrl</code> to <code>href</code>",
                        Fixes = new FixAction[]
                        {
                            new RenameAttributeFix(navigateUrl, "href")
                        }
                    };
                }
            }
            else if (tagToken.FindAttribute("RouteName") is { } routeNameAttribute
                     && !tagToken.TagName.StartsWith("webforms:"))
            {
                yield return new Suggestion()
                {
                    Description = "Change to <code>&lt;webforms:HybridRouteLink&gt;</code>",
                    Fixes = new FixAction[]
                    {
                        new RenameElementFix(tagToken, "webforms:HybridRouteLink", reader.EndTag)
                    }
                };
            }
        }
    }
}