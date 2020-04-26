using System;
using System.Collections.Generic;
using System.Text;
using DotVVM.Utils.AspxConverter.Parser.Tokens;

namespace DotVVM.Utils.AspxConverter.Matching
{
    public interface IMatcher
    {

        List<SuggestionInstance> TryConsume(List<AspxToken> token, int index);

    }
}
