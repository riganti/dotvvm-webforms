<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DotVVM.WebForms.Tutorial.WebFormsSamples._14_fileupload.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            
            <asp:FileUpload ID="ImageFileUpload" runat="server" />
            
            <asp:Button ID="UploadButton" runat="server" 
                        Text="Store Image"
                        OnClick="UploadButton_OnClick"/>

        </div>
    </form>
</body>
</html>
