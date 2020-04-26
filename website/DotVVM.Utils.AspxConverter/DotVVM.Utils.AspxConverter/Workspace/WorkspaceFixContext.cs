using System;
using System.Collections.Generic;
using System.Linq;
using DotVVM.Utils.AspxConverter.Matching;
using DotVVM.Utils.AspxConverter.Parser.Tokens;

namespace DotVVM.Utils.AspxConverter.Workspace
{
    public class WorkspaceFixContext
    {
        public Suggestion Suggestion { get; }
        public FixAction Fix { get; }
        public List<AspxToken> Tokens { get; }
        public List<CsharpFileBuilder> CsharpFiles { get; }

        public WorkspaceFixContext(Suggestion suggestion, FixAction fix, List<AspxToken> tokens, List<CsharpFileBuilder> csharpFiles)
        {
            Suggestion = suggestion;
            Fix = fix;
            Tokens = tokens;
            CsharpFiles = csharpFiles;
        }

        public CsharpFileBuilder GetViewModelFile()
        {
            var file = CsharpFiles.FirstOrDefault(f => f.ClassName == "ViewModel");
            if (file == null)
            {
                file = new CsharpFileBuilder() { ClassName = "ViewModel" };
                CsharpFiles.Add(file);
            }
            return file;
        }
    }
}