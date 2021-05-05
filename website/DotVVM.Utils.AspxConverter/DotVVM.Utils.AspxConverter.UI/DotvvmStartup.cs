using DotVVM.Framework.Configuration;
using DotVVM.Framework.ResourceManagement;
using DotVVM.Framework.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace DotVVM.Utils.AspxConverter.UI
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
            config.RouteTable.Add("Default", "", "Views/Default.dothtml");
            config.RouteTable.Add("Converter", "convert", "Views/Converter.dothtml");
        }

        private void ConfigureControls(DotvvmConfiguration config, string applicationPath)
        {
            config.Markup.AutoDiscoverControls(new DefaultControlRegistrationStrategy(config, "cc", "Controls"));
        }

        private void ConfigureResources(DotvvmConfiguration config, string applicationPath)
        {
            config.Resources.Register("converter", new ScriptResource(new UrlResourceLocation("~/js/converter.js"))
            {
                Dependencies = new[] { "converter-css" }
            });
            config.Resources.Register("converter-css", new StylesheetResource(new UrlResourceLocation("~/css/converter.min.css")));

            config.Resources.Register("homepage-css", new StylesheetResource(new UrlResourceLocation("~/css/homepage.min.css")));
            config.Resources.Register("nav-and-footer-css", new StylesheetResource(new UrlResourceLocation("~/css/nav-and-footer.min.css")));

            config.Resources.Register("syntax", new ScriptResource(new UrlResourceLocation("~/js/syntax/syntax.js")));
            config.Resources.Register("syntax-csharp", new ScriptResource(new UrlResourceLocation("~/js/syntax/syntax_csharp.js"))
            {
                Dependencies = new[] { "syntax" }
            });
            config.Resources.Register("syntax-dothtml", new ScriptResource(new UrlResourceLocation("~/js/syntax/syntax_dothtml.js"))
            {
                Dependencies = new[] { "syntax" }
            });
        }

        public void ConfigureServices(IDotvvmServiceCollection options)
        {
            options.AddDefaultTempStorages("temp");
        }
    }
}
