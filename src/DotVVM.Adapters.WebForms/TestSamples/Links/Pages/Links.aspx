<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Links.aspx.cs" Inherits="TestSamples.Links.Pages.Links" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        
        <p>
            <dotvvm:RouteLink runat="server" RouteName="Links_Test">
                Route with no params
            </dotvvm:RouteLink>
        </p>
        
        <p>
            <dotvvm:RouteLink runat="server" RouteName="Links_TestWithParams" Text="Route with hard-coded params" Param-title="title-15">
                <Parameters>
                    <asp:Parameter Name="id" DefaultValue="15"/>
                </Parameters>
            </dotvvm:RouteLink>
        </p>

        <p>
            <dotvvm:RouteLink runat="server" RouteName="Links_TestWithDefaults" Text="Route with data-bound and default params">
                <Parameters>
                    <asp:Parameter Name="id" DefaultValue="25"/>
                </Parameters>
            </dotvvm:RouteLink>
        </p>

        <asp:Repeater runat="server" ID="LinksRepeater">
            <ItemTemplate>
                <p>
                    <asp:HiddenField runat="server" ID="Hidden1" Value='<%# Eval("Id") %>'/>

                    <dotvvm:RouteLink runat="server" RouteName="Links_TestWithParams" Text="Route with data-bound params in Repeater" Param-Title='<%# Eval("Title") %>'>
                        <Parameters>
                            <asp:ControlParameter Name="id" ControlID="Hidden1" PropertyName="Value"/>
                        </Parameters>
                    </dotvvm:RouteLink>
                </p>
            </ItemTemplate>
        </asp:Repeater>

    </form>
</body>
</html>
