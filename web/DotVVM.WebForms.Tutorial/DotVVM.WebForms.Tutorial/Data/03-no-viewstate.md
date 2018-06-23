## No viewstate

-------------------------------------

ASP.NET stores the page state in a hidden field which is sent to the server on every postback. You can turn it on an off for any control, but some controls don't work when viewstate is disabled.

```DOTHTML
<input type="hidden" name="__VIEWSTATE" id="__VIEWSTATE" value="B9jhQnRecfXw4WHR9gGP2I+GO04KVMcekvzXe..." />
```

-------------------------------------

DotVVM keeps the page state in a [viewmodel](https://www.dotvvm.com/docs/tutorials/basics-viewmodels/2.0) defined by your own C# class. 

Thanks to that, you can precisely control what is included in the page state, and [what properties](https://www.dotvvm.com/docs/tutorials/basics-binding-direction/2.0) will be transferred from the server to client and back.

```CSHARP
public class MyPageViewModel
{
    public string FilterText { get; set; }

    [Bind(Direction.ServerToClient)]
    public List<CustomerDTO> Customers { get; set; }

    ...
 
}
```