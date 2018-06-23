## Linking to Routes

-------------------------------------

ASP.NET allows to build hyperlink to routes registered in a route table. Routing is optional in ASP.NET Web Forms.

```DOTHTML
<asp:HyperLink ID="SampleLink" runat="server" 
               NavigateUrl="<%$ RouteUrl: RouteName=Sample12, Id=1 %>" 
               Text="Link" />
```

-------------------------------------

DotVVM ships with the `RouteLink` control which can do the same thing. [Routing](https://www.dotvvm.com/docs/tutorials/basics-routing/2.0) is mandatory in DotVVM - pages cannot be accessed using their file names in URL.

```DOTHTML
<dot:RouteLink RouteName="Sample12" Param-Id="1" 
               Text="Link" />
```
