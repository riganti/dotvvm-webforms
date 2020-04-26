## No `<form>`

-------------------------------------

ASP.NET Web Forms needs the entire page to be wrapped in the `<form>` element. Otherwise, the postbacks wouldn't work.

If you want to take advantage of AJAX, you need to use the `UpdatePanel` control.

```DOTHTML
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DotVVM.WebForms.Tutorial.WebFormsSamples._01_no_form.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
    </form>
</body>
</html>
```

-------------------------------------

There is no need to do that in DotVVM. Postbacks in DotVVM are using AJAX by default, and in many cases, you don't need to do full postback at all. There are [other ways](https://www.dotvvm.com/docs/tutorials/basics-optimizing-postbacks/2.0) which are more efficient and transfer less data.

```DOTHTML
@viewModel DotVVM.WebForms.Tutorial.DotvvmSamples._01_no_form.ViewModels.DefaultViewModel, DotVVM.WebForms.Tutorial

<!DOCTYPE html>

<html lang="en" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <title></title>
</head>
<body>


</body>
</html>
```