using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotVVM.Framework.ViewModel;

namespace DotVVM.WebForms.Tutorial.DotvvmSamples._12_linking_to_routes.ViewModels
{
    public class DefaultViewModel : DotvvmViewModelBase
    {

        [FromRoute("Id")]
        public int CurrentId { get; set; }

    }
}

