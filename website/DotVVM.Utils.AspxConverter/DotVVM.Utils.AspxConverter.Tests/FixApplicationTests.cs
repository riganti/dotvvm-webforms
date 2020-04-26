using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotVVM.Utils.AspxConverter.Matching;
using DotVVM.Utils.AspxConverter.Matching.Fixes;
using DotVVM.Utils.AspxConverter.Parser.Tokens;
using DotVVM.Utils.AspxConverter.Workspace;
using Xunit;

namespace DotVVM.Utils.AspxConverter.Tests
{
    public class FixApplicationTests
    {
        private ConverterWorkspace workspace;

        [Fact]
        public void RemoveAttributeFixTest()
        {
            var input = @"<div runat=""server""></div>";
            InitWorkspace(input);
            
            var tag = (BeginTagToken) workspace.Tokens[0];
            ApplyFix(new RemoveAttributeFix(tag, tag.FindAttribute("runat")));

            var newMarkup = workspace.GetMarkup();
            Assert.Equal(@"<div></div>", newMarkup);
        }

        [Fact]
        public void RemoveTokenFixTest()
        {
            var input = @"<div runat=""server""></div>";
            InitWorkspace(input);

            ApplyFix(new RemoveTokenFix(workspace.Tokens[0]));

            var newMarkup = workspace.GetMarkup();
            Assert.Equal(@"</div>", newMarkup);
        }

        [Fact]
        public void RenameAttributeFixTest()
        {
            var input = @"<div  runat  = server></div>";
            InitWorkspace(input);

            var tag = (BeginTagToken)workspace.Tokens[0];
            ApplyFix(new RenameAttributeFix(tag.FindAttribute("runat"), "xxx"));

            var newMarkup = workspace.GetMarkup();
            Assert.Equal(@"<div  xxx  = server></div>", newMarkup);
        }

        [Fact]
        public void SetAttributeCommandBindingTest()
        {
            var input = @"<asp:TextBox />";
            InitWorkspace(input);

            var tag = (BeginTagToken)workspace.Tokens[0];
            ApplyFix(new SetAttributeCommandBindingFix(tag, "Changed", "OnTextChanged"));

            var newMarkup = workspace.GetMarkup();
            Assert.Equal(@"<asp:TextBox Changed=""{command: OnTextChanged()}"" />", newMarkup);
        }

        [Fact]
        public void SetAttributeValueBindingTest()
        {
            var input = @"<asp:TextBox/>";
            InitWorkspace(input);

            var tag = (BeginTagToken)workspace.Tokens[0];
            ApplyFix(new SetAttributeValueBindingFix(tag, "Text", "FirstName"));

            var newMarkup = workspace.GetMarkup();
            Assert.Equal(@"<asp:TextBox Text=""{value: FirstName}""/>", newMarkup);
        }

        [Fact]
        public void MoveTokenRangeFixTest()
        {
            var input = @"<asp:Repeater>
    <HeaderTemplate>
        <table>
    </HeaderTemplate>
</asp:Repeater>";
            InitWorkspace(input);

            var headerTemplateStart = workspace.Tokens[3];
            ApplyFix(new MoveTokenRangeFix(headerTemplateStart, 3, workspace.Tokens[0], Placement.Before));

            var newMarkup = workspace.GetMarkup();
            Assert.Equal(@"
        <table>
    <asp:Repeater>
    <HeaderTemplate></HeaderTemplate>
</asp:Repeater>", newMarkup);
        }

        private void InitWorkspace(string input)
        {
            workspace = new ConverterWorkspace();
            workspace.Initialize(input);
        }

        private void ApplyFix(FixAction fix)
        {
            var context = new WorkspaceFixContext(null, fix, workspace.Tokens, workspace.CsharpFiles);
            fix.Apply(context);
        }
    }
}
