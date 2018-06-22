<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DotVVM.WebForms.Tutorial.WebFormsSamples._10_textbox_and_validation.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            
            <asp:TextBox ID="BeginDateTextBox" runat="server" />
            -
            <asp:TextBox ID="EndDateTextBox" runat="server" />

            <asp:RequiredFieldValidator ID="BeginDateRequiredValidator" runat="server" 
                                        ErrorMessage="BeginDate is required!" 
                                        ControlToValidate="BeginDateTextBox"
                                        Display="Dynamic" />
            <asp:CompareValidator ID="BeginDateFormatValidator" runat="server" 
                                  ErrorMessage="BeginDate format is not correct!" 
                                  ControlToValidate="BeginDateTextBox" 
                                  Operator="DataTypeCheck" 
                                  Type="Date" 
                                  Display="Dynamic" />
            <asp:RequiredFieldValidator ID="EndDateRequiredValidator" runat="server" 
                                        ErrorMessage="EndDate is required!" 
                                        ControlToValidate="EndDateTextBox" 
                                        Display="Dynamic" />
            <asp:CompareValidator ID="EndDateFormatValidator" runat="server" 
                                  ErrorMessage="EndDate format is not correct!" 
                                  ControlToValidate="EndDateTextBox" 
                                  Operator="DataTypeCheck" 
                                  Type="Date" 
                                  Display="Dynamic" />
            <asp:CustomValidator ID="BeginEndDateValidator" runat="server" 
                                 ErrorMessage="EndDate must be greater than BeginDate!"
                                 ControlToValidate="BeginDateTextBox"
                                 OnServerValidate="BeginEndDateValidator_OnServerValidate" 
                                 Display="Dynamic" />
            

            <asp:Button ID="TestButton" runat="server" Text="Button" />

        </div>
    </form>
</body>
</html>
