## Master Pages

-------------------------------------

ASP.NET uses `ContentPlaceHolder` and `Content` controls to define a template and a dynamic content.

### Site.master

```DOTHTML
<!DOCTYPE html>

<html>
<head runat="server">
    <title></title>

    <asp:ContentPlaceHolder ID="HeadContent" runat="server">
    </asp:ContentPlaceHolder>

</head>
<body>
    <form id="form1" runat="server">
        <div>

            <asp:ContentPlaceHolder ID="MainContent" runat="server">
            </asp:ContentPlaceHolder>
            
        </div>
    </form>
</body>
</html>
```

### Default.aspx

```DOTHTML
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

</asp:Content>
```

-------------------------------------

DotVVM uses the same control names. If you want to build a [Single Page Application](https://www.dotvvm.com/docs/tutorials/basics-single-page-applications-spa/2.0), you can just use `SpaContentPlaceHolder`.

### Site.dotmaster

```DOTHTML
<!DOCTYPE html>

<html lang="en" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <title></title>

    <dot:ContentPlaceHolder ID="HeadContent" />

</head>
<body>

    <dot:SpaContentPlaceHolder ID="MainContent" />

</body>
</html>
```

### Default.dothtml

```DOTHTML
<dot:Content ContentPlaceHolderID="HeadContent">

</dot:Content>

<dot:Content ContentPlaceHolderID="MainContent">

</dot:Content>
```