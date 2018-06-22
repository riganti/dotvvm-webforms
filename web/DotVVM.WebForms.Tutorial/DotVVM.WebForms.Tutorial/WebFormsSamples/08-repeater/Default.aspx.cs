using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotVVM.WebForms.Tutorial.Model;

namespace DotVVM.WebForms.Tutorial.WebFormsSamples._08_repeater
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_PreRender(object sender, EventArgs e)
        {
            ItemsRepeater.DataSource = GetCollection();
            ItemsRepeater.DataBind();
        }

        private List<TextDTO> GetCollection()
        {
            return new List<TextDTO>()
            {
                new TextDTO() { Text = "aaa" },
                new TextDTO() { Text = "bbb" },
                new TextDTO() { Text = "ccc" }
            };
        }
    }
}