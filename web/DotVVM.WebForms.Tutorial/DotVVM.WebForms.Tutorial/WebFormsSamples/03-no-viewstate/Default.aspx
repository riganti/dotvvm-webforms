<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DotVVM.WebForms.Tutorial.WebFormsSamples._03_no_viewstate.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            
            <asp:TextBox runat="server" ID="FilterText" />
            
            <asp:Repeater runat="server" ID="CustomerList">
                <ItemTemplate>
                    <%# Container.DataItem %>
                </ItemTemplate>
            </asp:Repeater>

        </div>
    </form>
</body>
</html>
