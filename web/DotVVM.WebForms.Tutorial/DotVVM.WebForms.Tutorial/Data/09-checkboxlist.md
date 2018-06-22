## CheckBoxList

-------------------------------------

ASP.NET `CheckBoxList` control can return a collection of IDs, but the collection is not strongly-typed.

### Default.aspx

```DOTHTML
<asp:CheckBoxList ID="CountryList" runat="server"
                  DataTextField="Name" 
                  DataValueField="Id" />
```

### Default.aspx.cs

```CSHARP
// read selected item IDs
var selectedIds = CountryList.Items.OfType<ListItem>()
    .Where(i => i.Selected)
    .Select(i => int.Parse(i.Value))
    .ToList();
```

-------------------------------------

DotVVM `CheckBox` can be bound to a typed collection of values. Values that are checked will appear in the collection.

### Default.dothtml

```DOTHTML
<dot:Repeater DataSource="{value: Countries}">
    <dot:CheckBox Text="{value: Name}" CheckedValue="{value: Id}"
                  CheckedItems="{value: _root.SelectedIds}" />
</dot:Repeater>
```

### DefaultViewModel.cs

```CSHARP
public List<int> SelectedIds { get; set; } = new List<int>();
```