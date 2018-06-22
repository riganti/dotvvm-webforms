<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DotVVM.WebForms.Tutorial.WebFormsSamples._05_data_binding_everywhere.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        
        <%-- You need to call DataBind() manually, or place the control in another data-bound control. --%>
        <asp:TextBox ID="FirstNameTextBox" runat="server" Text="<%# FirstName %>" />

    </div>
    </form>
</body>
</html>
