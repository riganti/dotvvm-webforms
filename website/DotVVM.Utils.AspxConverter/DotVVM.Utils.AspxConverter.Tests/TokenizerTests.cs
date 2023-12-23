using System;
using System.Collections.Generic;
using System.Linq;
using DotVVM.Utils.AspxConverter.Parser;
using DotVVM.Utils.AspxConverter.Parser.Tokens;
using Xunit;

namespace DotVVM.Utils.AspxConverter.Tests
{
    public class TokenizerTests
    {
        private AspxTokenizer tokenizer;
        private List<AspxToken> tokens;
        private int checkedToken;

        public TokenizerTests()
        {
            tokenizer = new AspxTokenizer();
        }

        [Fact]
        public void EmptyInput()
        {
            var input = @"";
            Tokenize(input);

            CheckEnd();
        }

        [Fact]
        public void TextOnly()
        {
            var input = @"hello world";
            Tokenize(input);

            CheckToken<LiteralToken>(@"hello world");
            CheckEnd();
        }

        [Fact]
        public void DirectiveAndText()
        {
            var input = @" <%@ Page AutoEventsWireup=""true"" %>
text";
            Tokenize(input);

            CheckToken<LiteralToken>(@" ");
            CheckToken<DirectiveToken>(@"<%@ Page AutoEventsWireup=""true"" %>");
            CheckToken<LiteralToken>(@"
text");
            CheckEnd();
        }

        [Fact]
        public void TagOnly()
        {
            var input = @"<div>";
            Tokenize(input);

            CheckToken<BeginTagToken>(@"<div>", t =>
            {
                Assert.False(t.IsSelfClosing);
                Assert.Equal("div", t.TagName);
            });
            CheckEnd();
        }

        [Fact]
        public void TextAndTagOnly()
        {
            var input = @" g <div> te";
            Tokenize(input);

            CheckToken<LiteralToken>(@" g ");
            CheckToken<BeginTagToken>(@"<div>", t =>
            {
                Assert.False(t.IsSelfClosing);
                Assert.Equal("div", t.TagName);
            });
            CheckToken<LiteralToken>(@" te");
            CheckEnd();
        }

        [Fact]
        public void TextAndTwoTags()
        {
            var input = @" g <div>< / div >";
            Tokenize(input);

            CheckToken<LiteralToken>(@" g ");
            CheckToken<BeginTagToken>(@"<div>", t =>
            {
                Assert.False(t.IsSelfClosing);
                Assert.Equal("div", t.TagName);
            });
            CheckToken<EndTagToken>(@"< / div >", t =>
            {
                Assert.Equal("div", t.TagName);
            });
            CheckEnd();
        }

        [Fact]
        public void TextAndSelfClosingTag()
        {
            var input = @" g < div / >";
            Tokenize(input);

            CheckToken<LiteralToken>(@" g ");
            CheckToken<BeginTagToken>(@"< div / >", t =>
            {
                Assert.True(t.IsSelfClosing);
                Assert.Equal("div", t.TagName);
            });
            CheckEnd();
        }

        [Fact]
        public void TagWithNonValueAttribute()
        {
            var input = @"<div attr >";
            Tokenize(input);

            CheckToken<BeginTagToken>(@"<div attr >", t =>
            {
                Assert.False(t.IsSelfClosing);
                Assert.Equal("div", t.TagName);

                Assert.Single(t.Attributes);
                Assert.Equal("attr", t.Attributes[0].Name);
                Assert.IsType<AttributeEmptyValueToken>(t.Attributes[0].Value);
            });
            CheckEnd();
        }

        [Fact]
        public void TagWithNonValueAttributes()
        {
            var input = @"<div attr attr2 />";
            Tokenize(input);

            CheckToken<BeginTagToken>(@"<div attr attr2 />", t =>
            {
                Assert.True(t.IsSelfClosing);
                Assert.Equal("div", t.TagName);

                Assert.Equal(2, t.Attributes.Count);

                Assert.Equal("attr", t.Attributes[0].Name);
                Assert.IsType<AttributeEmptyValueToken>(t.Attributes[0].Value);

                Assert.Equal("attr2", t.Attributes[1].Name);
                Assert.IsType<AttributeEmptyValueToken>(t.Attributes[1].Value);
            });
            CheckEnd();
        }

        [Fact]
        public void TagWithUnquotedAttribute()
        {
            var input = @"< div    attr  =   val >";
            Tokenize(input);

            CheckToken<BeginTagToken>(@"< div    attr  =   val >", t =>
            {
                Assert.False(t.IsSelfClosing);
                Assert.Equal("div", t.TagName);

                Assert.Single(t.Attributes);

                Assert.Equal("attr", t.Attributes[0].Name);
                var value = Assert.IsType<AttributeUnquotedValueToken>(t.Attributes[0].Value);
                Assert.Equal("val", value.Value);
            });
            CheckEnd();
        }

        [Fact]
        public void TagWithQuotedAttribute()
        {
            var input = @"< div    attr  = '  val' >";
            Tokenize(input);

            CheckToken<BeginTagToken>(@"< div    attr  = '  val' >", t =>
            {
                Assert.False(t.IsSelfClosing);
                Assert.Equal("div", t.TagName);

                Assert.Single(t.Attributes);

                Assert.Equal("attr", t.Attributes[0].Name);
                var value = Assert.IsType<AttributeQuotedValueToken>(t.Attributes[0].Value);
                Assert.Equal("  val", value.Value);
            });
            CheckEnd();
        }

        [Fact]
        public void TagWithDoubleQuotedAttribute()
        {
            var input = @"< div    attr  = ""  val"" >";
            Tokenize(input);

            CheckToken<BeginTagToken>(@"< div    attr  = ""  val"" >", t =>
            {
                Assert.False(t.IsSelfClosing);
                Assert.Equal("div", t.TagName);

                Assert.Single(t.Attributes);

                Assert.Equal("attr", t.Attributes[0].Name);
                var value = Assert.IsType<AttributeQuotedValueToken>(t.Attributes[0].Value);
                Assert.Equal("  val", value.Value);
            });
            CheckEnd();
        }

        [Fact]
        public void ScriptSection()
        {
            var input = @"<script runat=""server""><<<<</ script>";
            Tokenize(input);

            CheckToken<BeginTagToken>(@"<script runat=""server"">", t =>
            {
                Assert.False(t.IsSelfClosing);
                Assert.Equal("script", t.TagName);

                Assert.Single(t.Attributes);

                Assert.Equal("runat", t.Attributes[0].Name);
                var value = Assert.IsType<AttributeQuotedValueToken>(t.Attributes[0].Value);
                Assert.Equal("server", value.Value);
            });
            CheckToken<LiteralToken>(@"<<<<");
            CheckToken<EndTagToken>(@"</ script>", t =>
            {
                Assert.Equal("script", t.TagName);
            });
            CheckEnd();
        }

        [Fact]
        public void UnclosedTag()
        {
            var input = @"<div <div";
            Tokenize(input);

            CheckToken<BeginTagToken>(@"<div ", t =>
            {
                Assert.False(t.IsSelfClosing);
                Assert.Equal("div", t.TagName);
            });
            CheckToken<BeginTagToken>(@"<div", t =>
            {
                Assert.False(t.IsSelfClosing);
                Assert.Equal("div", t.TagName);
            });
            CheckEnd();
        }

        [Fact]
        public void BindingInsideText()
        {
            var input = @"<p>some text <%# Eval(""Title"") %></p>";
            Tokenize(input);

            CheckToken<BeginTagToken>(@"<p>", t =>
            {
                Assert.False(t.IsSelfClosing);
                Assert.Equal("p", t.TagName);
            });
            CheckToken<LiteralToken>(@"some text ", t =>
            {
                Assert.Equal("some text ", t.Text);
            });
            CheckToken<BindingBlockToken>(@"<%# Eval(""Title"") %>", t =>
            {
                Assert.Equal(@" Eval(""Title"") ", t.Content);
            });
            CheckToken<EndTagToken>(@"</p>", t =>
            {
                Assert.Equal("p", t.TagName);
            });
            CheckEnd();
        }

        private void CheckEnd()
        {
            Assert.Equal(checkedToken, tokens.Count);
        }

        private void CheckToken<TToken>(string text, Action<TToken> assert = null) where TToken : AspxToken 
        {
            var token = Assert.IsType<TToken>(tokens[checkedToken]);
            Assert.Equal(tokens[checkedToken].Fragment, text);

            assert?.Invoke(token);
            checkedToken++;
        }

        private void Tokenize(string input)
        {
            tokens = tokenizer.Tokenize(input).ToList();
        }
    }
}
