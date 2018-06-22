using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DotVVM.WebForms.Tutorial.WebFormsSamples._05_data_binding_everywhere
{
    public partial class Default : System.Web.UI.Page
    {

        public string FirstName { get; set; } = "John";

        protected void Page_Load(object sender, EventArgs e)
        {
            this.DataBind();
        }
    }
}