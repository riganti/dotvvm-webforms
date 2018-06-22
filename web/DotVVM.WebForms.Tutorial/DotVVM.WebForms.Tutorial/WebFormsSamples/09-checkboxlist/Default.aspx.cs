using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotVVM.WebForms.Tutorial.Model;

namespace DotVVM.WebForms.Tutorial.WebFormsSamples._09_checkboxlist
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CountryList.DataSource = new[]
            {
                new CountryDTO() { Id = 1, Name = "Czech Republic" },
                new CountryDTO() { Id = 2, Name = "Germany" },
                new CountryDTO() { Id = 3, Name = "USA" }
            };
            CountryList.DataBind();

            // read selected item IDs
            var selectedIds = CountryList.Items.OfType<ListItem>()
                .Where(i => i.Selected)
                .Select(i => int.Parse(i.Value))
                .ToList();
        }
    }
}