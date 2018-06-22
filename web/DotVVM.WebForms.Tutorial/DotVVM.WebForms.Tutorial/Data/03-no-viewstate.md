## No viewstate

-------------------------------------

ASP.NET stores its state in a hidden field which is sent to the server on every postback. Some controls don't work when viewstate is disabled.

```DOTHTML
<input type="hidden" name="__VIEWSTATE" id="__VIEWSTATE" value="B9jhQnRecfXw4WHR9gGP2I+GO04KVMcekvzXe..." />
```

-------------------------------------

DotVVM keeps the page state in a viewmodel. It is your own C# class and can precisely control what is your state and what properties will be transferred from the server to client and back.

```CSHARP
public class MyPageViewModel
{
    public string FilterText { get; set; }

    [Bind(Direction.ServerToClient)]
    public List<CustomerDTO> Customers { get; set; }

    ...
 
}
```