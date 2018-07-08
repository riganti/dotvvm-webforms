using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotVVM.Adapters.WebForms.Controls.DotVVM;
using DotVVM.Framework.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DotVVM.Adapters.WebForms
{
    public static class DotvvmConfigurationExtensions
    {

        public static void AddWebFormsAdapterControls(this DotvvmConfiguration config)
        {
            config.Markup.AddCodeControls("webforms", typeof(RouteLink));
        }

    }
}
