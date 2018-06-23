## No `runat="server"` and control IDs

-------------------------------------

All controls in ASP.NET need to specify `runat="server"` to work properly, and often you need to specify their ID.

```DOTHTML
<asp:Button ID="Button1" runat="server" Text="Hello" />
```

-------------------------------------

DotVVM doesn't have the `runat` attribute and even control IDs are not necessary. DotVVM uses the [MVVM pattern](https://www.dotvvm.com/docs/tutorials/introduction/2.0) and data-binding. Thus, you don't need to access the controls from your code.

```DOTHTML
<dot:Button Text="Hello" />
```

