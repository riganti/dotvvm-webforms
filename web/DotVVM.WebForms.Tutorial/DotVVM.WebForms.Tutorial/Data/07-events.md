## Events

-------------------------------------

ASP.NET controls provide various events that can be handled in code-behind.

### Default.aspx

```DOTHTML
<asp:Button ID="SaveButton" runat="server" Text="Save" OnClick="SaveButton_Click" />
```

### Default.aspx.cs

```CSHARP
protected void SaveButton_Click(object sender, EventArgs e)
{
    // ...
}
```

-------------------------------------

DotVVM controls can call methods in the viewmodel. Because the viewmodel doesn't depend on the view, it is testable. You can also pass any arguments to the methods in the viewmodel. Async methods are also supported.

### Default.dothtml

```DOTHTML
<dot:Button Text="Save" Click="{command: Save()}" />
```

### DefaultViewModel.cs

```CSHARP
public void Save()
{
    // ...
}
```