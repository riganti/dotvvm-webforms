using System;
using System.Collections.Generic;
using System.Linq;

namespace DotVVM.Utils.AspxConverter.Workspace
{
    public class SuggestionGroupData
    {

        public string Description { get; set; }

        public string HelpUrl { get; set; }

        public List<SuggestionGroupItemData> Suggestions { get; set; }

    }
}