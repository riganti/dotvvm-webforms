<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DotVVM.WebForms.Tutorial.WebFormsSamples._12_linking_to_routes.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            
            <asp:HyperLink ID="SampleLink" runat="server" NavigateUrl="<%$ RouteUrl: RouteName=Sample12, Id=1 %>" Text="Link" />
            
            Route Parameter: <asp:Literal runat="server" ID="CurrentID" />

        </div>
    </form>
</body>
</html>
