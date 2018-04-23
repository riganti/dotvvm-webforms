using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotVVM.Framework.ViewModel;

namespace TestSamples.Links.ViewModels
{
    public class LinksViewModel : DotvvmViewModelBase
    {

        public int TestId => 20;

        public string TestTitle => "title-20";

        public TitleData[] Titles => new[]
        {
            new TitleData() { Id = 10, Title = "title-10" },
            new TitleData() { Id = 40, Title = "title-40" }
        };

    }
}
