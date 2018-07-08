using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DotVVM.Adapters.WebForms.Controls.WebForms
{
    [
        ControlBuilder(typeof(HyperLinkControlBuilder)),
        DefaultProperty("Text"),
        ToolboxData("<{0}:RouteLink runat=\"server\" RouteName=\"\">HyperLink</{0}:RouteLink>"),
        ParseChildren(true, DefaultProperty = nameof(ContentTemplate))
    ]
    public class RouteLink : WebControl
    {
        public RouteLink() : base(HtmlTextWriterTag.A)
        {
        }

        [
            Bindable(true),
            Category("Navigation"),
            DefaultValue("")
        ]
        public string RouteName
        {
            get
            {
                string s = (string)ViewState["RouteName"];
                return s ?? String.Empty;
            }
            set
            {
                ViewState["RouteName"] = value;
            }
        }

        public override bool SupportsDisabledAttribute => true;

        [
            Category("Navigation"),
            DefaultValue(""),
            TypeConverter(typeof(TargetConverter))
        ]
        public string Target
        {
            get
            {
                string s = (string)ViewState["Target"];
                return s ?? String.Empty;
            }
            set
            {
                ViewState["Target"] = value;
            }
        }


        [
            Localizable(true),
            Bindable(true),
            Category("Appearance"),
            DefaultValue("")
        ]
        public virtual string Text
        {
            get
            {
                object o = ViewState["Text"];
                return ((o == null) ? String.Empty : (string)o);
            }
            set
            {
                if (HasControls())
                {
                    Controls.Clear();
                }
                ViewState["Text"] = value;
            }
        }

        [
            Category("Appearance"),
            PersistenceMode(PersistenceMode.InnerDefaultProperty)
        ]
        public ITemplate ContentTemplate { get; set; }


        [
            DefaultValue(null),
            MergableProperty(false),
            PersistenceMode(PersistenceMode.InnerProperty),
            Category("Data")
        ]
        public ParameterCollection Parameters { get; } = new ParameterCollection();


        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            if (Enabled && !IsEnabled && SupportsDisabledAttribute)
            {
                // copied from asp:HyperLink implementation
                writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled");
            }

            // grab and remove Param-something from the Attributes collection
            var attributeParameterKeys = Attributes.Keys.OfType<string>().Where(p => p.StartsWith("Param-", StringComparison.OrdinalIgnoreCase)).ToList();
            var attributeParameters = attributeParameterKeys.ToDictionary(k => k.Substring("Param-".Length), k => Attributes[k]);
            foreach (var key in attributeParameterKeys)
            {
                Attributes.Remove(key);
            }

            base.AddAttributesToRender(writer);

            // render href and target attributes
            string s = ComposeRouteUrl(RouteName, Parameters, attributeParameters);
            if ((s.Length > 0) && IsEnabled)
            {
                string resolvedUrl = ResolveClientUrl(s);
                writer.AddAttribute(HtmlTextWriterAttribute.Href, resolvedUrl);
            }

            s = Target;
            if (s.Length > 0)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Target, s);
            }
        }

        protected override void OnInit(EventArgs e)
        {
            if (ContentTemplate != null)
            {
                ContentTemplate.InstantiateIn(this);
            }

            base.OnInit(e);
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            if (HasControls())
            {
                base.RenderContents(writer);
            }
            else
            {
                writer.Write(Text);
            }
        }


        private string ComposeRouteUrl(string routeName, ParameterCollection parameters, Dictionary<string, string> attributeParameters)
        {
            // compose DotVVM route URL
            var config = Context.GetDotvvmConfiguration();
            
            var route = config.RouteTable[routeName];

            var routeParameters = new Dictionary<string, object>();
            foreach (var value in Page.RouteData.Values)
            {
                routeParameters[value.Key] = value.Value;
            }
            foreach (DictionaryEntry value in parameters.GetValues(Context, this))
            {
                routeParameters[value.Key.ToString()] = value.Value;
            }
            foreach (var value in attributeParameters)
            {
                routeParameters[value.Key] = value.Value;
            }

            return route.BuildUrl(routeParameters);
        }

        
    }
}
