using System;
using System.Collections.Generic;
using System.Linq;
using DotVVM.Utils.AspxConverter.Workspace;

namespace DotVVM.Utils.AspxConverter.Matching
{
    public abstract class FixAction
    {
        public abstract void Apply(WorkspaceFixContext context);
    }
}