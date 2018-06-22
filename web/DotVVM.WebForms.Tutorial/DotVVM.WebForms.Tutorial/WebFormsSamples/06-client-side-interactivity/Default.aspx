<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DotVVM.WebForms.Tutorial.WebFormsSamples._06_client_side_interactivity.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            
            <asp:CheckBox ID="IsOtherItemCheckBox" runat="server" 
                          AutoPostBack="true"
                          OnCheckedChanged="IsOtherItemCheckBox_CheckedChanged"
                          Text="Other" CausesValidation="false" />

            <asp:TextBox ID="OtherValueTextBox" runat="server" Enabled="false" />

        </div>
    </form>
</body>
</html>
