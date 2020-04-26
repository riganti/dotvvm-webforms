using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using CommonMark.Syntax;
using DotVVM.Framework.Binding;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using DotVVM.Utils.AspxConverter.UI.Services;

namespace DotVVM.Utils.AspxConverter.UI.Controls
{
    public class Sample : DotvvmMarkupControl
    {
        private readonly SampleRenderer sampleRenderer;


        [MarkupOptions(AllowBinding = false, Required = true)]
        public string SampleName
        {
            get { return (string)GetValue(SampleNameProperty); }
            set { SetValue(SampleNameProperty, value); }
        }
        public static readonly DotvvmProperty SampleNameProperty
            = DotvvmProperty.Register<string, Sample>(c => c.SampleName, null);


        public SampleDTO Data { get; set; }

        public Sample(SampleRenderer sampleRenderer)
        {
            this.sampleRenderer = sampleRenderer;
        }

        protected override void OnInit(IDotvvmRequestContext context)
        {
            Data = sampleRenderer.GetSampleHtml(SampleName);

            base.OnInit(context);
        }
    }

}