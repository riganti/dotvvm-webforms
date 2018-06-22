## No `<form>`

-------------------------------------

ASP.NET Web Forms needs the entire page to be wrapped in the `<form>` element. Otherwise, the postbacks wouldn't work.

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

-------------------------------------

There is no need to do that in DotVVM. Postbacks are using AJAX and send JSON-serialized viewmodel to the server, and there are also [other ways](https://www.dotvvm.com/docs/tutorials/basics-optimizing-postbacks/2.0) which transfer much less data.

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