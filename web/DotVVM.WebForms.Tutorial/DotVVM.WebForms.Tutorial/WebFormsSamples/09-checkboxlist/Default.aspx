<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DotVVM.WebForms.Tutorial.WebFormsSamples._09_checkboxlist.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            
            <asp:CheckBoxList ID="CountryList" runat="server"
                              DataTextField="Name" 
                              DataValueField="Id" />
            
        </div>
    </form>
</body>
</html>
