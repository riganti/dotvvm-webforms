using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;
using DotVVM.Framework.Binding;
using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;

namespace DotVVM.Adapters.WebForms.Controls.DotVVM
{
    public class RouteLink : HtmlGenericControl
    {
        /// <summary>
        /// Gets or sets the name of the route in System.Web.Routing route table.
        /// </summary>
        [MarkupOptions(AllowBinding = false)]
        public string RouteName
        {
            get { return (string)GetValue(RouteNameProperty); }
            set { SetValue(RouteNameProperty, value); }
        }

        public static readonly DotvvmProperty RouteNameProperty
            = DotvvmProperty.Register<string, RouteLink>(c => c.RouteName, null);


        /// <summary>
        /// Gets or sets the suffix that will be appended to the generated URL (e.g. query string or URL fragment).
        /// </summary>
        public string UrlSuffix
        {
            get { return (string)GetValue(UrlSuffixProperty); }
            set { SetValue(UrlSuffixProperty, value); }
        }
        public static readonly DotvvmProperty UrlSuffixProperty
            = DotvvmProperty.Register<string, RouteLink>(c => c.UrlSuffix, null);

        /// <summary>
        /// Gets or sets the text of the hyperlink.
        /// </summary>
        [MarkupOptions(AllowBinding = false)]
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        public static readonly DotvvmProperty TextProperty
            = DotvvmProperty.Register<string, RouteLink>(c => c.Text, null);

        public bool Enabled
        {
            get { return (bool)GetValue(EnabledProperty); }
            set { SetValue(EnabledProperty, value); }
        }
        public static readonly DotvvmProperty EnabledProperty =
            DotvvmProperty.Register<bool, RouteLink>(t => t.Enabled, true);



        public VirtualPropertyGroupDictionary<object> Params { get; private set; }

        public static DotvvmPropertyGroup ParamsGroupDescriptor = 
            DotvvmPropertyGroup.Register<object, RouteLink>("Param-", "Params");

        public VirtualPropertyGroupDictionary<object> QueryParameters { get; private set; }

        public static DotvvmPropertyGroup QueryParametersGroupDescriptor =
            DotvvmPropertyGroup.Register<object, RouteLink>("Query-", "QueryParameters");


        public RouteLink() : base("a")
        {
            Params = new VirtualPropertyGroupDictionary<object>(this, ParamsGroupDescriptor);
            QueryParameters = new VirtualPropertyGroupDictionary<object>(this, QueryParametersGroupDescriptor);
        }

        protected override void AddAttributesToRender(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            var parameters = new RouteValueDictionary();

            foreach (var param in context.Parameters)
            {
                parameters[param.Key] = param.Value;
            }
            foreach (var item in Params.RawValues)
            {
                parameters[item.Key] = item.Value;
            }

            var url = RouteTable.Routes.GetVirtualPath(null, RouteName, parameters).VirtualPath;
            writer.AddAttribute("href", url);

            WebFormsRouteLinkHelpers.WriteRouteLinkHrefAttribute(this, writer, context);

            base.AddAttributesToRender(writer, context);
        }


        protected override void RenderContents(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            if (HasOnlyWhiteSpaceContent())
            { 
                writer.WriteText(Text);
            }
            else
            {
                base.RenderContents(writer, context);
            }
        }
    }
}
