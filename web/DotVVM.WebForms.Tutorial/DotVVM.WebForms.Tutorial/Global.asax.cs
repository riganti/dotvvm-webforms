using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace DotVVM.WebForms.Tutorial
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            RouteTable.Routes.MapPageRoute("Sample1", "webforms/sample1", "~/WebFormsSamples/01-no-form/Default.aspx");
            RouteTable.Routes.MapPageRoute("Sample2", "webforms/sample2", "~/WebFormsSamples/02-no-runat-server-and-control-ids/Default.aspx");
            RouteTable.Routes.MapPageRoute("Sample3", "webforms/sample3", "~/WebFormsSamples/03-no-viewstate/Default.aspx");
            RouteTable.Routes.MapPageRoute("Sample4", "webforms/sample4", "~/WebFormsSamples/04-master-pages/Default.aspx");
            RouteTable.Routes.MapPageRoute("Sample5", "webforms/sample5", "~/WebFormsSamples/05-data-binding-everywhere/Default.aspx");
            RouteTable.Routes.MapPageRoute("Sample6", "webforms/sample6", "~/WebFormsSamples/06-client-side-interactivity/Default.aspx");
            RouteTable.Routes.MapPageRoute("Sample7", "webforms/sample7", "~/WebFormsSamples/07-eventsm/Default.aspx");
            RouteTable.Routes.MapPageRoute("Sample8", "webforms/sample8", "~/WebFormsSamples/08-repeater/Default.aspx");
            RouteTable.Routes.MapPageRoute("Sample9", "webforms/sample9", "~/WebFormsSamples/09-checkboxlist/Default.aspx");
            RouteTable.Routes.MapPageRoute("Sample10", "webforms/sample10", "~/WebFormsSamples/10-textbox-and-validation/Default.aspx");
            RouteTable.Routes.MapPageRoute("Sample11", "webforms/sample11", "~/WebFormsSamples/11-localization/Default.aspx");
            RouteTable.Routes.MapPageRoute("Sample12", "webforms/sample12/{Id}", "~/WebFormsSamples/12-linking-to-routes/Default.aspx", true, new RouteValueDictionary() { { "Id", "" }});
            RouteTable.Routes.MapPageRoute("Sample13", "webforms/sample13", "~/WebFormsSamples/13-gridview/Default.aspx");
            RouteTable.Routes.MapPageRoute("Sample14", "webforms/sample14", "~/WebFormsSamples/14-fileupload/Default.aspx");
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}