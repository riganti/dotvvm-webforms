using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotVVM.Utils.AspxConverter.Parser;
using DotVVM.Utils.AspxConverter.Parser.Tokens;
using Xunit;

namespace DotVVM.Utils.AspxConverter.Tests
{
    public class ControlInnerElementsReaderTests
    {
        
        [Fact]
        public void EmptyTag()
        {
            var input = @"<div></div>";
            var reader = CreateReader(input);

            Assert.Equal(0, reader.Elements.Count);
        }

        [Fact]
        public void UnclosedTag()
        {
            var input = @"<div>";
            var reader = CreateReader(input);

            Assert.Equal(0, reader.Elements.Count);
        }

        [Fact]
        public void SelfClosingTag()
        {
            var input = @"<div />";
            var reader = CreateReader(input);

            Assert.Equal(0, reader.Elements.Count);
        }

        [Fact]
        public void SingleSelfClosingContentTag()
        {
            var input = @"<asp:Repeater>
    <ItemTemplate />
</asp:Repeater>";
            var reader = CreateReader(input);

            Assert.Equal(1, reader.Elements.Count);
            
            Assert.Equal(1, reader.Elements["ItemTemplate"].Count);

            var itemTemplate = Assert.IsType<BeginTagToken>(reader.Elements["ItemTemplate"][0]);
            Assert.Equal("ItemTemplate", itemTemplate.TagName);
            Assert.True(itemTemplate.IsSelfClosing);
        }

        [Fact]
        public void SingleClosingContentTag()
        {
            var input = @"<asp:Repeater>
    <ItemTemplate>
test
</ItemTemplate>
</asp:Repeater>";
            var reader = CreateReader(input);

            Assert.Equal(1, reader.Elements.Count);
            {
                Assert.Equal(3, reader.Elements["ItemTemplate"].Count);

                var itemTemplate = Assert.IsType<BeginTagToken>(reader.Elements["ItemTemplate"][0]);
                Assert.Equal("ItemTemplate", itemTemplate.TagName);
                Assert.False(itemTemplate.IsSelfClosing);

                var literal = Assert.IsType<LiteralToken>(reader.Elements["ItemTemplate"][1]);
                Assert.Equal(@"
test
", literal.Fragment);

                var itemTemplateEnd = Assert.IsType<EndTagToken>(reader.Elements["ItemTemplate"][2]);
                Assert.Equal("ItemTemplate", itemTemplateEnd.TagName);
            }
        }

        [Fact]
        public void TwoContentTags()
        {
            var input = @"<asp:Repeater>
    <ItemTemplate>
test
</ItemTemplate>
<SeparatorTemplate>, </SeparatorTemplate>
</asp:Repeater>";
            var reader = CreateReader(input);

            Assert.Equal(2, reader.Elements.Count);
            {
                Assert.Equal(3, reader.Elements["ItemTemplate"].Count);

                var itemTemplate = Assert.IsType<BeginTagToken>(reader.Elements["ItemTemplate"][0]);
                Assert.Equal("ItemTemplate", itemTemplate.TagName);
                Assert.False(itemTemplate.IsSelfClosing);

                var literal = Assert.IsType<LiteralToken>(reader.Elements["ItemTemplate"][1]);
                Assert.Equal(@"
test
", literal.Fragment);

                var itemTemplateEnd = Assert.IsType<EndTagToken>(reader.Elements["ItemTemplate"][2]);
                Assert.Equal("ItemTemplate", itemTemplateEnd.TagName);
            }
            {
                Assert.Equal(3, reader.Elements["SeparatorTemplate"].Count);

                var separatorTemplate = Assert.IsType<BeginTagToken>(reader.Elements["SeparatorTemplate"][0]);
                Assert.Equal("SeparatorTemplate", separatorTemplate.TagName);
                Assert.False(separatorTemplate.IsSelfClosing);

                var literal2 = Assert.IsType<LiteralToken>(reader.Elements["SeparatorTemplate"][1]);
                Assert.Equal(@", ", literal2.Fragment);

                var separatorTemplateEnd = Assert.IsType<EndTagToken>(reader.Elements["SeparatorTemplate"][2]);
                Assert.Equal("SeparatorTemplate", separatorTemplateEnd.TagName);
            }
        }

        [Fact]
        public void UnclosedTagsInHierarchy()
        {
            var input = @"<asp:Repeater id=""Repeater1"" runat=""server"">
          <HeaderTemplate>
             <table border=""1"">
                <tr>
                   <td><b>Company</b></td>
                   <td><b>Symbol</b></td>
                </tr>
          </HeaderTemplate>
             
          <ItemTemplate>
             <tr>
                <td> <%# DataBinder.Eval(Container.DataItem, ""Name"") %> </td>
                <td> <%# DataBinder.Eval(Container.DataItem, ""Ticker"") %> </td>
             </tr>
          </ItemTemplate>
             
          <FooterTemplate>
             </table>
          </FooterTemplate>
             
       </asp:Repeater>";
            var reader = CreateReader(input);

            Assert.Equal(3, reader.Elements.Count);
            {
                Assert.Equal(21, reader.Elements["HeaderTemplate"].Count);
            }
            {
                Assert.Equal(19, reader.Elements["ItemTemplate"].Count);
            }
            {
                Assert.Equal(5, reader.Elements["FooterTemplate"].Count);
            }
        }

        private ControlInnerElementsReader CreateReader(string input)
        {
            var tokenizer = new AspxTokenizer();
            var tokens = tokenizer.Tokenize(input).ToList();
            return new ControlInnerElementsReader(tokens, 0);
        }
    }
}
