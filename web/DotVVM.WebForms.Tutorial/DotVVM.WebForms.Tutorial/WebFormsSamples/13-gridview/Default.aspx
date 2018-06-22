<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DotVVM.WebForms.Tutorial.WebFormsSamples._13_gridview.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            
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
            
        </div>
    </form>
</body>
</html>
