<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DotVVM.WebForms.Tutorial.WebFormsSamples._08_repeater.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            
            <asp:Repeater ID="ItemsRepeater" runat="server">
                <HeaderTemplate><ul></HeaderTemplate>
                <ItemTemplate>
                    <li><%# Eval("Text") %></li>
                </ItemTemplate>
                <FooterTemplate></ul></FooterTemplate>
            </asp:Repeater>

        </div>
    </form>
</body>
</html>
