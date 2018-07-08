using System;
using System.Collections.Generic;
using System.Linq;
using DotVVM.Adapters.WebForms;
using DotVVM.Framework.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TestSamples
{
    public class DotvvmStartup : IDotvvmStartup, IDotvvmServiceConfigurator
    {
        public void Configure(DotvvmConfiguration config, string applicationPath)
        {
            config.AddWebFormsAdapterControls();

            RegisterRoutes(config, applicationPath);
            RegisterControls(config, applicationPath);
            RegisterResources(config, applicationPath);
        }

        private void RegisterRoutes(DotvvmConfiguration config, string applicationPath)
        {
            config.RouteTable.Add("Links", "dot-links", "Links/Views/Links.dothtml");
            config.RouteTable.Add("Links_Test", "dot-links-test", "Links/Views/Links.dothtml");
            config.RouteTable.Add("Links_TestWithParams", "dot-links-test/{id}/{title}", "Links/Views/Links.dothtml");
            config.RouteTable.Add("Links_TestWithDefaults", "dot-links-test/{id}/{title?}", "Links/Views/Links.dothtml", new { title = "default" });
        }

        private void RegisterControls(DotvvmConfiguration config, string applicationPath)
        {
            
        }

        private void RegisterResources(DotvvmConfiguration config, string applicationPath)
        {
            
        }

        public void ConfigureServices(IDotvvmServiceCollection services)
        {
        }
    }
}