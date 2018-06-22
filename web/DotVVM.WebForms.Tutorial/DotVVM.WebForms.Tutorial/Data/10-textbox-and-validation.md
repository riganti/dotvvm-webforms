## TextBox and Validation

-------------------------------------

ASP.NET provides a wide range of validator controls that can display error messages. The validation rules are defined in the page markup.

### Default.aspx

```DOTHTML
<asp:TextBox ID="BeginDateTextBox" runat="server" />
-
<asp:TextBox ID="EndDateTextBox" runat="server" />

<asp:RequiredFieldValidator ID="BeginDateRequiredValidator" runat="server" 
                            ErrorMessage="BeginDate is required!" 
                            ControlToValidate="BeginDateTextBox"
                            Display="Dynamic" />
<asp:CompareValidator ID="BeginDateFormatValidator" runat="server" 
                        ErrorMessage="BeginDate format is not correct!" 
                        ControlToValidate="BeginDateTextBox" 
                        Operator="DataTypeCheck" 
                        Type="Date" 
                        Display="Dynamic" />
<asp:RequiredFieldValidator ID="EndDateRequiredValidator" runat="server" 
                            ErrorMessage="EndDate is required!" 
                            ControlToValidate="EndDateTextBox" 
                            Display="Dynamic" />
<asp:CompareValidator ID="EndDateFormatValidator" runat="server" 
                        ErrorMessage="EndDate format is not correct!" 
                        ControlToValidate="EndDateTextBox" 
                        Operator="DataTypeCheck" 
                        Type="Date" 
                        Display="Dynamic" />
<asp:CustomValidator ID="BeginEndDateValidator" runat="server" 
                        ErrorMessage="EndDate must be greater than BeginDate!"
                        ControlToValidate="BeginDateTextBox"
                        OnServerValidate="BeginEndDateValidator_OnServerValidate" 
                        Display="Dynamic" />
```

### Default.aspx.cs

```CSHARP
protected void BeginEndDateValidator_OnServerValidate(object source, ServerValidateEventArgs args)
{
    if (DateTime.TryParse(BeginDateTextBox.Text, out var beginDate) && DateTime.TryParse(EndDateTextBox.Text, out var endDate))
    {
        if (beginDate > endDate)
        {
            args.IsValid = false;
            return;
        }
    }

    args.IsValid = true;
}
```

-------------------------------------

DotVVM takes advantage of Data Annotation Attributes and supports `IValidatableObject` for custom validation rules. The validation rules are defined in the viewmodel. 

DotVVM validators can also apply CSS classes on any HTML elements and much more.

### Default.dothtml

```DOTHTML
<dot:TextBox Text="{value: BeginDate}" FormatString="d" />
-
<dot:TextBox Text="{value: EndDate}" FormatString="d" />

<dot:ValidationSummary />
```

### TimeSlot.cs

```CSHARP
public class DefaultViewModel : DotvvmViewModelBase, IValidatableObject
{

    [Required]
    [DotvvmEnforceClientFormat]     // display an error if the format is not correct (without this attribute, BeginDate would be DateTime.MinValue, or null in case of Nullable<DateTime>)
    public DateTime BeginDate { get; set; }

    [Required]
    [DotvvmEnforceClientFormat]
    public DateTime EndDate { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (BeginDate > EndDate)
        {
            yield return this.CreateValidationResult("EndDate must be greater than BeginDate!", vm => vm.BeginDate);
        }
    }

}
```