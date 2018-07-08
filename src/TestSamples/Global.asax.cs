using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace TestSamples
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            RegisterRoutes(System.Web.Routing.RouteTable.Routes);
        }

        private void RegisterRoutes(RouteCollection routes)
        {
            routes.MapPageRoute("Links", "asp-links", "~/Links/Pages/Links.aspx");
            routes.MapPageRoute("Links_Test", "asp-links-test", "~/Links/Pages/Links.aspx");
            routes.MapPageRoute("Links_TestWithParams", "asp-links-test/{id}/{title}", "~/Links/Pages/Links.aspx");
            routes.MapPageRoute("Links_TestWithDefaults", "asp-links-test/{id}/{title}", "~/Links/Pages/Links.aspx", true, new RouteValueDictionary() { { "title", "default" } });
        }
    }
}