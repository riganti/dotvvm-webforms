using DotVVM.Framework.Configuration;
using DotVVM.Framework.ResourceManagement;
using DotVVM.Framework.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace DotVVM.WebForms.Tutorial
{
    public class DotvvmStartup : IDotvvmStartup, IDotvvmServiceConfigurator
    {
        // For more information about this class, visit https://dotvvm.com/docs/tutorials/basics-project-structure
        public void Configure(DotvvmConfiguration config, string applicationPath)
        {
            ConfigureRoutes(config, applicationPath);
            ConfigureControls(config, applicationPath);
            ConfigureResources(config, applicationPath);
        }

        private void ConfigureRoutes(DotvvmConfiguration config, string applicationPath)
        {
            config.RouteTable.Add("Default", "", "Home/Views/Default.dothtml");

            config.RouteTable.Add("Sample1", "dotvvm/sample1", "DotvvmSamples/01-no-form/Views/Default.dothtml");
            config.RouteTable.Add("Sample2", "dotvvm/sample2", "DotvvmSamples/02-no-runat-server-and-control-ids/Views/Default.dothtml");
            config.RouteTable.Add("Sample3", "dotvvm/sample3", "DotvvmSamples/03-no-viewstate/Views/Default.dothtml");
            config.RouteTable.Add("Sample4", "dotvvm/sample4", "DotvvmSamples/04-master-pages/Views/Default.dothtml");
            config.RouteTable.Add("Sample5", "dotvvm/sample5", "DotvvmSamples/05-data-binding-everywhere/Views/Default.dothtml");
            config.RouteTable.Add("Sample6", "dotvvm/sample6", "DotvvmSamples/06-client-side-interactivity/Views/Default.dothtml");
            config.RouteTable.Add("Sample7", "dotvvm/sample7", "DotvvmSamples/07-eventsm/Views/Default.dothtml");
            config.RouteTable.Add("Sample8", "dotvvm/sample8", "DotvvmSamples/08-repeater/Views/Default.dothtml");
            config.RouteTable.Add("Sample9", "dotvvm/sample9", "DotvvmSamples/09-checkboxlist/Views/Default.dothtml");
            config.RouteTable.Add("Sample10", "dotvvm/sample10", "DotvvmSamples/10-textbox-and-validation/Views/Default.dothtml");
            config.RouteTable.Add("Sample11", "dotvvm/sample11", "DotvvmSamples/11-localization/Views/Default.dothtml");
            config.RouteTable.Add("Sample12", "dotvvm/sample12/{Id?}", "DotvvmSamples/12-linking-to-routesm/Views/Default.dothtml");
            config.RouteTable.Add("Sample13", "dotvvm/sample13", "DotvvmSamples/13-gridview/Views/Default.dothtml");
            config.RouteTable.Add("Sample14", "dotvvm/sample14", "DotvvmSamples/14-fileupload/Views/Default.dothtml");
        }

        private void ConfigureControls(DotvvmConfiguration config, string applicationPath)
        {
            // register code-only controls and markup controls
            config.Markup.AddMarkupControl("cc", "Sample", "Home/Controls/Sample.dotcontrol");
        }

        private void ConfigureResources(DotvvmConfiguration config, string applicationPath)
        {
            // register custom resources and adjust paths to the built-in resources
        }

        public void ConfigureServices(IDotvvmServiceCollection options)
        {
            options.AddDefaultTempStorages("temp");
        }
    }
}
