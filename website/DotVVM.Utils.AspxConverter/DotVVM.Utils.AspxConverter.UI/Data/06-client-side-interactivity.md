## Client-side Interactivity

-------------------------------------

The data-binding expressions are evaluated on the server. If you want to disable a `TextBox` based on `CheckBox` value, you need to make a postback. Alternatively, you write a piece of JavaScript and deal with client IDs. 

### Default.aspx

```DOTHTML
<asp:CheckBox ID="IsOtherItemCheckBox" runat="server" 
                AutoPostBack="true"
                OnCheckedChanged="IsOtherItemCheckBox_CheckedChanged"
                Text="Other" 
                CausesValidation="false" />

<asp:TextBox ID="OtherValueTextBox" runat="server" Enabled="false" />
```

### Default.aspx.cs

```CSHARP
protected void IsOtherItemCheckBox_CheckedChanged(object sender, EventArgs e)
{
    OtherValueTextBox.Enabled = IsOtherItemCheckBox.Checked;
}
```

-------------------------------------

In DotVVM, the binding expressions are translated to JavaScript and evaluated directly in the browser. You can also use expressions in [value bindings](https://www.dotvvm.com/docs/tutorials/basics-value-binding/2.0).

### Default.dothtml

```DOTHTML
<dot:CheckBox Checked="{value: IsOtherItem}" Text="Other" />
<dot:TextBox Enabled="{value: IsOtherItem}" Text="{value: OtherValue}" />
```

### DefaultViewModel.cs

```CSHARP
public bool IsOtherItem { get; set; }
public string OtherValue { get; set; }
```