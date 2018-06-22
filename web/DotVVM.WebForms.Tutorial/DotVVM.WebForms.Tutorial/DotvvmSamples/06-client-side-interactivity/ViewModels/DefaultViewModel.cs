using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotVVM.Framework.ViewModel;

namespace DotVVM.WebForms.Tutorial.DotvvmSamples._06_client_side_interactivity.ViewModels
{
    public class DefaultViewModel : DotvvmViewModelBase
    {

        public bool IsOtherItem { get; set; }
        public string OtherValue { get; set; }

    }
}

