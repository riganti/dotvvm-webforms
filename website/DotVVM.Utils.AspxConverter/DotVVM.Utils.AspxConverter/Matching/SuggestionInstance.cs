using System;
using System.Collections.Generic;
using System.Linq;

namespace DotVVM.Utils.AspxConverter.Matching
{
    public class SuggestionInstance
    {

        public Suggestion Suggestion { get; set; }

        public int TagIndex { get; set; }

        public int? AttributeIndex { get; set; }

        public int Index { get; set; }

        public string SpanIndex => AttributeIndex != null ? $"{TagIndex}-{AttributeIndex}" : $"{TagIndex}";

        public string UniqueId => $"{SpanIndex}#{Index}";
    }
}