## GridView

-------------------------------------

ASP.NET includes the `GridView` control with plenty of features. Some data sources support even server-side sorting and paging.

### Default.aspx

```DOTHTML
<asp:GridView runat="server" ID="ProductsGrid" 
                AutoGenerateColumns="false"
                ItemType="DotVVM.WebForms.Tutorial.Model.ProductDTO"
                SelectMethod="GetData"
                AllowSorting="true" 
                AllowPaging="true" PageSize="20">
    <Columns>
        <asp:CheckBoxField DataField="IsSelected" />
        <asp:BoundField HeaderText="Product" DataField="Title" SortExpression="Title" />
        <asp:BoundField HeaderText="Category" DataField="Category" SortExpression="Category" />
        <asp:BoundField HeaderText="Qty/Unit" DataField="QuantityPerUnit" SortExpression="QuantityPerUnit" />
        <asp:BoundField HeaderText="Unit Price" DataField="UnitPrice" DataFormatString="c" SortExpression="UnitPrice" />
        <asp:BoundField HeaderText="In Stock" DataField="UnitsInStock" SortExpression="UnitsInStock" />
    </Columns>
</asp:GridView>
```

### Default.aspx.cs

```CSHARP
public IQueryable<ProductDTO> GetData()
{
    // ...
}
```

-------------------------------------

DotVVM contains a similar `GridView` control. It can be populated manually, or using `IQueryable`. 

The control can be styled easily using CSS, and the paging is done by a separate `DataPager` control.

### Default.dothtml

```DOTHTML
<dot:GridView DataSource="{value: Products}">
    <dot:GridViewCheckBoxColumn ValueBinding="{value: IsSelected}" />
    <dot:GridViewTextColumn HeaderText="Product" ValueBinding="{value: Title}" AllowSorting="true" />
    <dot:GridViewTextColumn HeaderText="Category" ValueBinding="{value: Category}" AllowSorting="true" />
    <dot:GridViewTextColumn HeaderText="Qty/Unit" ValueBinding="{value: QuantityPerUnit}" AllowSorting="true" />
    <dot:GridViewTextColumn HeaderText="Unit Price" ValueBinding="{value: UnitPrice}" FormatString="c" AllowSorting="true" />
    <dot:GridViewTextColumn HeaderText="In Stock" ValueBinding="{value: UnitsInStock}" AllowSorting="true" />
</dot:GridView>

<dot:DataPager DataSet="{value: Products}" />
```

### DefaultViewModel.cs

```CSHARP
public GridViewDataSet<ProductDTO> Products { get; set; } = new GridViewDataSet<ProductDTO>()
{
    PagingOptions =
    {
        PageSize = 20
    }
};

public override Task PreRender()
{
    IQueryable<ProductDTO> queryable = GetProducts();
    Products.LoadFromQueryable(queryable);

    return base.PreRender();
}
```