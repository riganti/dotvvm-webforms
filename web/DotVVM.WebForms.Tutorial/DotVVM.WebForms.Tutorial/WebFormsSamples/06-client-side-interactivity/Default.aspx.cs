using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DotVVM.WebForms.Tutorial.WebFormsSamples._06_client_side_interactivity
{
    public partial class Default : System.Web.UI.Page
    {

        protected void IsOtherItemCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            OtherValueTextBox.Enabled = IsOtherItemCheckBox.Checked;
        }
    }
}