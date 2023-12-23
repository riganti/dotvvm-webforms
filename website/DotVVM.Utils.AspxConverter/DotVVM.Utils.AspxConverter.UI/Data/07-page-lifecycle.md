## Page Lifecycle

-------------------------------------

ASP.NET pages fire various events before the HTML output is rendered - `Init`, `Load` and `PreRender`.

### Default.aspx.cs

```CSHARP
protected void Page_Init(object sender, EventArgs e)
{
    // ...
}

protected void Page_Load(object sender, EventArgs e)
{
    // ...
}

protected void Page_PreRender(object sender, EventArgs e)
{
    // ...
}
```

-------------------------------------

DotVVM has a [similar ](https://www.dotvvm.com/docs/latest/pages/concepts/viewmodels/overview#viewmodel-lifecycle) lifecycle. 
The semantics of the methods is the same, and they are async.

### DefaultViewModel.cs

```CSHARP
public override Task Init()
{
    // init default values for the viewmodel
}

public override Task Load()
{
    // the viewmodel is ready for a command to be invoked
}

public override Task PreRender()
{
    // the command has been invoked, we prepare everything
}
```

> It is recommended to load data from the database in the `PreRender` stage.