## Repeater

-------------------------------------

ASP.NET `Repeater` control can be populated using data source controls or from a code-behind. You can use `HeaderTemplate` and `FooterTemplate` to wrap the items in a custom element.

### Default.aspx

```DOTHTML
<asp:Repeater ID="ItemsRepeater" runat="server">
    <HeaderTemplate><ul></HeaderTemplate>
    <ItemTemplate>
        <li><%# Eval("Text") %></li>
    </ItemTemplate>
    <FooterTemplate></ul></FooterTemplate>
</asp:Repeater>
```

### Default.aspx.cs

```CSHARP
protected void Page_PreRender(object sender, EventArgs e)
{
    ItemsRepeater.DataSource = GetCollection();
    ItemsRepeater.DataBind();
}
```

-------------------------------------

DotVVM `Repeater` is similar. The `ItemTemplate` element is optional and you can specify the wrapper tag using `WrapperTagName` property. And there's data-binding.

### Default.dothtml

```DOTHTML
<dot:Repeater DataSource="{value: Items}" WrapperTagName="ul">
    <li>{{value: Text}}</li>
</dot:Repeater>
```

### DefaultViewModel.cs

```CSHARP
public List<TextDTO> Items { get; set; }
                
public override Task PreRender()
{
    Items = GetCollection();

    return base.PreRender();
}
```