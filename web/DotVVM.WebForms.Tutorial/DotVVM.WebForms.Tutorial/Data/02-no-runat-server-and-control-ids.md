## No `runat="server"` and control IDs

-------------------------------------

All ASP.NET controls need to specify `runat="server"` to function properly.

```DOTHTML
<asp:Button ID="Button1" runat="server" Text="Hello" />
```

-------------------------------------

DotVVM doesn't have the `runat` attribute - it is not needed. Even control IDs are not necessary.

```DOTHTML
<dot:Button Text="Hello" />
```

