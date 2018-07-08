using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.Hosting;

namespace DotVVM.Adapters.WebForms
{
    public static class HttpContextExtensions
    {

        public static DotvvmConfiguration GetDotvvmConfiguration(this HttpContext context)
        {
            var owinContext = context.GetOwinContext();
            if (!owinContext.Environment.TryGetValue(HostingConstants.DotvvmRequestContextOwinKey, out var result) || !(result is DotvvmRequestContext))
            {
                throw new InvalidOperationException("Couldn't find DotVVM configuration object in the OWIN environment dictionary! Make sure DotVVM has been registered by calling app.UseDotVVM<...>(); on startup.");
            }

            return ((DotvvmRequestContext) result).Configuration;
        }

    }
}
