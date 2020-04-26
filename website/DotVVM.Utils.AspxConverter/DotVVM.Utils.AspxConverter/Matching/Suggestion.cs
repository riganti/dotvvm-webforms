using System;
using System.Collections.Generic;
using System.Linq;

namespace DotVVM.Utils.AspxConverter.Matching
{
    public class Suggestion
    {

        public string Description { get; set; }

        public string HelpUrl { get; set; }

        public IReadOnlyList<FixAction> Fixes { get; set; }
        
    }
}