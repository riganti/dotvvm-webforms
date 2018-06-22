## Localization

-------------------------------------

ASP.NET provided a way to use RESX files for localization of controls.

```DOTHTML
<asp:Button ID="SaveButton" runat="server" Text="<%$ Resources: MyResources, SaveButtonText %>" />
```

-------------------------------------

DotVVM supports the resource binding for RESX files or other C# expressions (e.g. ASP.NET Core localization).

```DOTHTML
<dot:Button Text="{resource: MyResources.SaveButtonText}" />
```
