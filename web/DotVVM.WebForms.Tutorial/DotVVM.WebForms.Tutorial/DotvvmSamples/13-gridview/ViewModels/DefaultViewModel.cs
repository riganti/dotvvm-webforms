using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotVVM.Framework.Controls;
using DotVVM.Framework.ViewModel;
using DotVVM.WebForms.Tutorial.Model;

namespace DotVVM.WebForms.Tutorial.DotvvmSamples._13_gridview.ViewModels
{
    public class DefaultViewModel : DotvvmViewModelBase
    {

        public GridViewDataSet<ProductDTO> Products { get; set; } = new GridViewDataSet<ProductDTO>()
        {
            PagingOptions =
            {
                PageSize = 20
            }
        };

        public override Task PreRender()
        {
            var queryable = TestDatabase.GetProducts();
            Products.LoadFromQueryable(queryable);

            return base.PreRender();
        }
    }
}

