using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotVVM.Framework.Binding;
using DotVVM.Framework.Controls;

namespace DotVVM.Utils.AspxConverter.UI.Controls.Icons
{
    public class Icon : DotvvmMarkupControl
    {
        [MarkupOptions(AllowBinding = true)]
        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }
        public static readonly DotvvmProperty NameProperty
            = DotvvmProperty.Register<string, Icon>(c => c.Name, null);
    }
}