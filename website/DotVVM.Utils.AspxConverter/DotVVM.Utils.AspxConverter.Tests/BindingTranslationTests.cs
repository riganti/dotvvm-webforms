using DotVVM.Utils.AspxConverter.Matching.Utils;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DotVVM.Utils.AspxConverter.Tests
{
    public class BindingTranslationTests
    {

        [Theory]
        [InlineData(@"Eval(""Title"")", @"Title")]
        [InlineData(@"Eval(""Title"", ""test {0}"")", @"string.Format(""test {0}"", Title)")]
        [InlineData(@"DataBinder.Eval(Container.DataItem, ""Title"")", @"Title")]
        [InlineData(@"DataBinder.Eval(Container.DataItem, ""Title"", ""test {0}"")", @"string.Format(""test {0}"", Title)")]
        [InlineData(@"Bind(""Title"")", @"Title")]
        [InlineData(@"Bind(""Title"", ""test {0}"")", @"string.Format(""test {0}"", Title)")]
        [InlineData(@"DataBinder.Bind(Container.DataItem, ""Title"")", @"Title")]
        [InlineData(@"DataBinder.Bind(Container.DataItem, ""Title"", ""test {0}"")", @"string.Format(""test {0}"", Title)")]
        [InlineData(@"(string)Eval(""Title"")", "(string)Title")]
        [InlineData(@"Eval((string)Eval(""PropertyName""))", "Eval((string)PropertyName)")]
        public void EvalBindMethods(string input, string expected)
        {
            var result = TranslateBinding(input);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(@"Item.Title", "Title")]
        [InlineData(@"BindItem.Description[0]", "Description[0]")]
        public void ItemBindItemMembers(string input, string expected)
        {
            var result = TranslateBinding(input);
            Assert.Equal(expected, result);
        }

        private string TranslateBinding(string input)
        {
            return BindingParser.TranslateBinding(input);
        }
    }
}
