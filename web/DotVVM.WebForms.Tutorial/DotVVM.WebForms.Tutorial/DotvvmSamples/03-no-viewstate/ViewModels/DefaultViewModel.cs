using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;
using DotVVM.WebForms.Tutorial.Model;

namespace DotVVM.WebForms.Tutorial.DotvvmSamples._03_no_viewstate.ViewModels
{
    public class DefaultViewModel : DotvvmViewModelBase
    {

        public string FilterText { get; set; }

        [Bind(Direction.ServerToClient)]
        public List<CustomerDTO> Customers { get; set; }


        public override Task PreRender()
        {
            Customers = new List<CustomerDTO>()
            {
                new CustomerDTO() { Name = "Customer 1" },
                new CustomerDTO() { Name = "Customer 2" }
            };

            return base.PreRender();
        }

    }
    
}

