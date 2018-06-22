using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotVVM.WebForms.Tutorial.Model;

namespace DotVVM.WebForms.Tutorial.WebFormsSamples._13_gridview
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public IQueryable<ProductDTO> GetData()
        {
            return TestDatabase.GetProducts();
        }
    }
}