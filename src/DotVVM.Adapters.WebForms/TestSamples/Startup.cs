using System;
using System.Threading.Tasks;
using System.Web.Hosting;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(TestSamples.Startup))]

namespace TestSamples
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseDotVVM<DotvvmStartup>(HostingEnvironment.ApplicationPhysicalPath);
        }
    }
}
