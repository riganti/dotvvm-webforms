using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using CommonMark.Syntax;
using DotVVM.Framework.Binding;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using DotVVM.WebForms.Tutorial.Home.Services;

namespace DotVVM.WebForms.Tutorial.Home.Controls
{
    public class Sample : DotvvmMarkupControl
    {

        [MarkupOptions(AllowBinding = false, Required = true)]
        public string SampleName
        {
            get { return (string)GetValue(SampleNameProperty); }
            set { SetValue(SampleNameProperty, value); }
        }
        public static readonly DotvvmProperty SampleNameProperty
            = DotvvmProperty.Register<string, Sample>(c => c.SampleName, null);


        public SampleDTO Data { get; set; }


        protected override void OnInit(IDotvvmRequestContext context)
        {
            Data = new SampleRenderer().GetSampleHtml(SampleName);

            base.OnInit(context);
        }
    }

}