using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotVVM.WebForms.Tutorial.Model;

namespace DotVVM.WebForms.Tutorial.WebFormsSamples._03_no_viewstate
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_PreRender(object sender, EventArgs e)
        {
            FilterText.Text = "My filter";

            CustomerList.DataSource = new List<CustomerDTO>()
            {
                new CustomerDTO() { Name = "Customer 1" },
                new CustomerDTO() { Name = "Customer 2" }
            };
            CustomerList.DataBind();
        }
    }
}