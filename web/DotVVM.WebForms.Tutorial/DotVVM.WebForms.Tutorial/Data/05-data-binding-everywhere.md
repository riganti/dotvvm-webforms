## Data-binding Everywhere

-------------------------------------

ASP.NET can use `Eval`, `Bind` or `Item.Property` in data-bound controls like `GridView`. Using `Eval` or `Bind` can lead to runtime errors.

### Default.aspx

```DOTHTML
<%-- You need to call DataBind() manually, or place the control in another data-bound control. --%>
<asp:TextBox ID="FirstNameTextBox" runat="server" Text="<%# FirstName %>" />
```

### Default.aspx.cs

```CSHARP
public string FirstName { get; set; } = "John";

protected void Page_Load(object sender, EventArgs e)
{
    this.DataBind();
}
```

-------------------------------------

DotVVM can do data-binding everywhere and it is always type-safe. Even complex types and collections are fully supported.

### Default.dothtml

```DOTHTML
<dot:TextBox Text="{value: FirstName}" />
```

### DefaultViewModel.cs

```CSHARP
public string FirstName { get; set; } = "John";
```