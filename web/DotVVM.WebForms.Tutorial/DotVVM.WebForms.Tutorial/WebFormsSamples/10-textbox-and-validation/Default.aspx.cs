using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DotVVM.WebForms.Tutorial.WebFormsSamples._10_textbox_and_validation
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

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
    }
}