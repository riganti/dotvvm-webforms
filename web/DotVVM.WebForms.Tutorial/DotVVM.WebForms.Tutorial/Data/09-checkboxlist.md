## CheckBoxList

-------------------------------------

ASP.NET `CheckBoxList` control can return a collection of IDs, but the collection is not strongly-typed and working with selected items is not very straight-forward.

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

DotVVM [CheckBox](https://www.dotvvm.com/docs/controls/builtin/CheckBox/2.0) can be bound to a single boolean property, or to a typed collection of values. Values which are checked will appear in the collection, and vice-versa.

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