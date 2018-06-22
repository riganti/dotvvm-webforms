using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.ViewModel;
using DotVVM.WebForms.Tutorial.Model;

namespace DotVVM.WebForms.Tutorial.DotvvmSamples._08_repeater.ViewModels
{
    public class DefaultViewModel : DotvvmViewModelBase
    {

        public List<TextDTO> Items { get; set; }

        public override Task PreRender()
        {
            Items = GetCollection();

            return base.PreRender();
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

