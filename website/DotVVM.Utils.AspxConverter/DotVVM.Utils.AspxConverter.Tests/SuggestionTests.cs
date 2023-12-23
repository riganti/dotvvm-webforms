using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using DotVVM.Utils.AspxConverter.Matching;
using DotVVM.Utils.AspxConverter.Workspace;
using Xunit;

namespace DotVVM.Utils.AspxConverter.Tests
{
    public class SuggestionTests
    {
        private ConverterWorkspace workspace;

        [Fact]
        public void ButtonSuggestionsTest()
        {
            var input = @"<asp:Button runat=""server"" Text=""Button"" OnClick=""Button1_Click"" ID=""Button1"">
</asp:Button>";
            InitWorkspace(input);

            ApplySuggestions();

            var markup = workspace.GetMarkup();
            Assert.Equal(@"<dot:Button Text=""Button"" ID=""Button1"" Click=""{command: OnButton1Click()}"">
</dot:Button>", markup);
        }

        [Fact]
        public void GridViewSuggestionsTest()
        {
            var input = @"<asp:GridView runat=""server"" ID=""GridView1""
    DataSourceID=""SqlDataSource1"">
    <Columns>
        <asp:BoundField HeaderText=""Col1"" DataField=""Test"" />
        <asp:TemplateField HeaderText=""Col2"">
            <ItemTemplate>
                <%# Eval(""Amount"") %> pcs
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>";
            InitWorkspace(input);

            ApplySuggestions();

            var markup = workspace.GetMarkup();
            Assert.Equal(@"<dot:GridView ID=""GridView1""
    DataSourceID=""SqlDataSource1"">
    <Columns>
        <dot:GridViewTextColumn HeaderText=""Col1"" ValueBinding=""{value: Test}"" />
        <dot:GridViewTemplateColumn HeaderText=""Col2"">
            <ContentTemplate>
                {{value: Eval(""Amount"")}} pcs
            </ContentTemplate>
        </dot:GridViewTemplateColumn>
    </Columns>
</dot:GridView>", markup);
        }

        [Fact]
        public void ImageTagSuggestionsTest()
        {
            var input = @"<asp:Image runat=""server"" ImageUrl=""abc.png"" AlternateText=""My Image"" />";
            InitWorkspace(input);

            ApplySuggestions();

            var markup = workspace.GetMarkup();
            Assert.Equal(@"<img src=""abc.png"" alt=""My Image"" />", markup);
        }

        [Fact]
        public void RepeaterSuggestionsTest()
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
          <td> <%# Eval(""Name"") %> </td>
          <td> <%# Eval(""Ticker"") %> </td>
       </tr>
    </ItemTemplate>
    <FooterTemplate>
       </table>
    </FooterTemplate>
</asp:Repeater>";
            InitWorkspace(input);

            ApplySuggestions();

            var markup = workspace.GetMarkup();
            Assert.Equal(@"
       <table border=""1"">
          <tr>
             <td><b>Company</b></td>
             <td><b>Symbol</b></td>
          </tr>
    <dot:Repeater id=""Repeater1"">
    
    <ItemTemplate>
       <tr>
          <td> {{value: Eval(""Name"")}} </td>
          <td> {{value: Eval(""Ticker"")}} </td>
       </tr>
    </ItemTemplate>
    
</dot:Repeater>
       </table>
    ", markup);
        }

        [Fact]
        public void UpdateProgressSuggestionsTest()
        {
            var input = @"<asp:UpdateProgress runat=""server"" id=""UpdateProgress1"">
    <ContentTemplate>
        Loading...
    </ContentTemplate>
</asp:UpdateProgress>";
            InitWorkspace(input);

            ApplySuggestions();

            var markup = workspace.GetMarkup();
            Assert.Equal(@"<dot:UpdateProgress id=""UpdateProgress1"">
    
        Loading...
    
</dot:UpdateProgress>", markup);
        }

        [Fact]
        public void ListViewSuggestionsTest()
        {
            var input = @"<asp:ListView runat=""server"" ItemType=""test"" SelectMethod=""SelectTags"">
        <LayoutTemplate>
            <article>
                <header>
                    List of categories
                </header>
                <ul class=""taglist"">
                    <asp:PlaceHolder ID=""ItemPlaceHolder"" runat=""server"" />
                </ul>
            </article>
        </LayoutTemplate>
        <ItemTemplate>
            <li><span class=""ui-icon ui-icon-tag""></span> <%# Item.TagName %></li>
        </ItemTemplate>
    </asp:ListView>";

            InitWorkspace(input);

            ApplySuggestions();

            var markup = workspace.GetMarkup();
            Assert.Equal(@"
            <article>
                <header>
                    List of categories
                </header>
                <ul class=""taglist"">
                    <dot:Repeater ItemType=""test"" SelectMethod=""SelectTags"">
        
        <ItemTemplate>
            <li><span class=""ui-icon ui-icon-tag""></span> {{value: Item.TagName}}</li>
        </ItemTemplate>
    </dot:Repeater>
                </ul>
            </article>
        ", markup);
        }

        [Fact]
        public void AttributeBindingSuggestionsTest()
        {
            var input = @"<h1 class='<%# Item.Title %>'>test</h1>";
            InitWorkspace(input);

            ApplySuggestions();

            var markup = workspace.GetMarkup();
            Assert.Equal(@"<h1 class=""{value: Item.Title}"">test</h1>", markup);
        }


        [Fact]
        public void TextBindingSuggestionsTest()
        {
            var input = @"some text <%# Item.Title %>";
            InitWorkspace(input);

            ApplySuggestions();

            var markup = workspace.GetMarkup();
            Assert.Equal(@"some text {{value: Item.Title}}", markup);
        }

        [Fact]
        public void HyperLinkSuggestionsTest()
        {
            var input = @"<asp:HyperLink NavigateUrl=""https://www.google.com"">Google</asp:HyperLink>";
            InitWorkspace(input);

            ApplySuggestions();

            var markup = workspace.GetMarkup();
            Assert.Equal(@"<a href=""https://www.google.com"">Google</a>", markup);
        }

        [Fact]
        public void HyperLinkWithTextSuggestionsTest()
        {
            var input = @"<asp:HyperLink NavigateUrl=""https://www.google.com"" Text=""My text"" />";
            InitWorkspace(input);

            ApplySuggestions();

            var markup = workspace.GetMarkup();
            Assert.Equal(@"<a href=""https://www.google.com"">My text</a>", markup);
        }

        [Fact]
        public void HyperLinkRouteSuggestionsTest()
        {
            var input = @"<asp:HyperLink NavigateUrl=""<%$ RouteUrl: RouteName=HomePage, PageIndex=1 %>"" Text=""My text"" />";
            InitWorkspace(input);

            ApplySuggestions();

            var markup = workspace.GetMarkup();
            Assert.Equal(@"<webforms:HybridRouteLink Text=""My text"" RouteName=""HomePage"" Param-PageIndex=""1"" />", markup);
        }

        private void InitWorkspace(string input)
        {
            workspace = new ConverterWorkspace();
            workspace.Initialize(input);
        }

        private void ApplySuggestions()
        {
            var suggestions = workspace.Suggestions.Select(s => s.UniqueId).ToArray();
            workspace.ApplyFixes(suggestions);
        }

    }
}
