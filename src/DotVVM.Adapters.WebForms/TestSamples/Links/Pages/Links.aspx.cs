using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TestSamples.Links.Pages
{
    public partial class Links : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LinksRepeater.DataSource = new[]
            {
                new TitleData() { Id = 10, Title = "title-10" },
                new TitleData() { Id = 40, Title = "title-40" }
            };
            LinksRepeater.DataBind();
        }
    }
}