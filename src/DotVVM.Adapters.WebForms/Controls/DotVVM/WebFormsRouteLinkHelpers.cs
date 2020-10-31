using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using DotVVM.Framework.Binding;
using DotVVM.Framework.Binding.Expressions;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.Routing;
using DotVVM.Framework.Utils;
using DotVVM.Framework.ViewModel.Serialization;
using Newtonsoft.Json;

namespace DotVVM.Adapters.WebForms.Controls.DotVVM
{
    public static class WebFormsRouteLinkHelpers
    {

        private static readonly JsonSerializerSettings jsonSettings = DefaultSerializerSettingsProvider.Instance.GetSettingsCopy();

        public static void WriteRouteLinkHrefAttribute(RouteLink control, IHtmlWriter writer, IDotvvmRequestContext context)
        {
            // Render client-side knockout expression only if there exists a parameter with value binding
            var containsBindingParam = control.Params.RawValues.Any(p => p.Value is IValueBinding);
            if (containsBindingParam)
            {
                var group = new KnockoutBindingGroup();
                group.Add("href", GenerateKnockoutHrefExpression(control.RouteName, control, context));
                writer.AddKnockoutDataBind("attr", group);
            }

            if (control.RenderOnServer || !containsBindingParam)
            {
                writer.AddAttribute("href", EvaluateRouteUrl(control.RouteName, control, context));
            }
        }

        public static string EvaluateRouteUrl(string routeName, RouteLink control, IDotvvmRequestContext context)
        {
            var urlSuffix = GenerateUrlSuffixCore(control.GetValue(RouteLink.UrlSuffixProperty) as string, control);
            var coreUrl = GenerateRouteUrlCore(routeName, control, context) + urlSuffix;

            return context.TranslateVirtualPath(coreUrl);
        }

        private static string GenerateRouteUrlCore(string routeName, RouteLink control, IDotvvmRequestContext context)
        {
            var route = GetRoute(context, routeName);
            var parameters = ComposeNewRouteParameters(control, context, route);

            // evaluate bindings on server
            foreach (var param in parameters.Where(p => p.Value is IStaticValueBinding).ToList())
            {
                EnsureValidBindingType(param.Value as BindingExpression);
                parameters[param.Key] = ((IValueBinding)param.Value).Evaluate(control);   // TODO: see below
            }

            // generate the URL
            return "~/" + route.GetVirtualPath(HttpContext.Current.Request.RequestContext, parameters)?.VirtualPath;
        }

        private static string GenerateUrlSuffixCore(string urlSuffix, RouteLink control)
        {
            // generate the URL suffix
            return UrlHelper.BuildUrlSuffix(urlSuffix, control.QueryParameters);
        }

        private static Route GetRoute(IDotvvmRequestContext context, string routeName)
        {
            return (Route)System.Web.Routing.RouteTable.Routes[routeName];
        }

        public static string GenerateKnockoutHrefExpression(string routeName, RouteLink control, IDotvvmRequestContext context)
        {
            var link = GenerateRouteLinkCore(routeName, control, context);

            var urlSuffix = GetUrlSuffixExpression(control);
            return $"'{context.TranslateVirtualPath("~/")}' + {link}{(urlSuffix == null ? "" : " + " + urlSuffix)}";
        }

        private static string GetUrlSuffixExpression(RouteLink control)
        {
            var urlSuffixBase =
                control.GetValueBinding(RouteLink.UrlSuffixProperty)
                    ?.Apply(binding => binding.GetKnockoutBindingExpression(control))
                ?? JsonConvert.SerializeObject(control.UrlSuffix ?? "");
            var queryParams =
                control.QueryParameters.RawValues.Select(p => TranslateRouteParameter(control, p, true)).StringJoin(",");

            // generate the function call
            return
                queryParams.Length > 0 ? $"dotvvm.buildUrlSuffix({urlSuffixBase}, {{{queryParams}}})" :
                urlSuffixBase != "\"\"" ? urlSuffixBase :
                null;
        }

        private static string GenerateRouteLinkCore(string routeName, RouteLink control, IDotvvmRequestContext context)
        {
            var route = GetRoute(context, routeName);
            var parameters = ComposeNewRouteParameters(control, context, route);

            var parametersExpression = parameters.Select(p => TranslateRouteParameter(control, p)).StringJoin(",");
            // generate the function call

            return $"dotvvm.buildRouteUrl({JsonConvert.ToString(route.Url)}, {{{parametersExpression}}})";
        }

        private static string TranslateRouteParameter(DotvvmBindableObject control, KeyValuePair<string, object> param, bool caseSensitive = false)
        {
            string expression = "";
            if (param.Value is IBinding)
            {
                EnsureValidBindingType(param.Value as IBinding);

                expression = (param.Value as IValueBinding)?.GetKnockoutBindingExpression(control)
                             ?? JsonConvert.SerializeObject((param.Value as IStaticValueBinding)?.Evaluate(control), jsonSettings);
            }
            else
            {
                expression = JsonConvert.SerializeObject(param.Value, jsonSettings);
            }
            return JsonConvert.SerializeObject(caseSensitive ? param.Key : param.Key.ToLower()) + ": " + expression;
        }

        private static void EnsureValidBindingType(IBinding binding)
        {
            if (!(binding is IStaticValueBinding))
            {
                throw new Exception("Only value bindings are supported in <dot:RouteLink Param-xxx='' /> attributes!");
            }
        }

        private static RouteValueDictionary ComposeNewRouteParameters(RouteLink control, IDotvvmRequestContext context, Route route)
        {
            var parameters = new RouteValueDictionary(route.Defaults?.ToDictionary(t => t.Key, t => t.Value) ?? new Dictionary<string, object>());
            foreach (var param in context.Parameters)
            {
                parameters[param.Key] = param.Value;
            }
            foreach (var item in control.Params.RawValues)
            {
                parameters[item.Key] = item.Value;
            }
            return parameters;
        }
    }
}