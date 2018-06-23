## Localization

-------------------------------------

ASP.NET provided a way to use RESX files for localization of controls.

```DOTHTML
<asp:Button ID="SaveButton" runat="server" Text="<%$ Resources: MyResources, SaveButtonText %>" />
```

-------------------------------------

DotVVM supports the [resource binding](https://www.dotvvm.com/docs/tutorials/basics-resource-binding/2.0) for RESX files or other C# expressions (e.g. ASP.NET Core localization).

```DOTHTML
<dot:Button Text="{resource: MyResources.SaveButtonText}" />
```
