using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotVVM.Framework.ViewModel;
using DotVVM.WebForms.Tutorial.Model;

namespace DotVVM.WebForms.Tutorial.DotvvmSamples._09_checkboxlist.ViewModels
{
    public class DefaultViewModel : DotvvmViewModelBase
    {

        [Bind(Direction.ServerToClientFirstRequest)]
        public List<CountryDTO> Countries => new List<CountryDTO>()
        {
            new CountryDTO() { Id = 1, Name = "Czech Republic" },
            new CountryDTO() { Id = 2, Name = "Germany" },
            new CountryDTO() { Id = 3, Name = "USA" }
        };

        public List<int> SelectedIds { get; set; }


    }
}

