using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;
using DotVVM.Framework.Hosting;
using DotVVM.Utils.AspxConverter.Workspace;

namespace DotVVM.Utils.AspxConverter.UI.ViewModels
{
    public class ConverterViewModel : MasterPageViewModel
    {
        private ConverterWorkspace workspace;
        private bool needsRefresh;


        public int TabIndex { get; set; }

        [Required]
        public string OriginalMarkup { get; set; }

        public string DisplayHtml { get; set; }

        public List<SuggestionGroupData> SuggestionGroups { get; set; }

        public List<CsharpFileBuilder> CsharpFiles { get; set; }

        public void SubmitCode()
        {
            TabIndex = 1;

            workspace = new ConverterWorkspace();
            workspace.Initialize(OriginalMarkup, CsharpFiles);
            needsRefresh = true;
        }
        
        public override Task PreRender()
        {
            if (needsRefresh)
            {
                OriginalMarkup = workspace.GetMarkup();
                DisplayHtml = workspace.DisplayHtml;
                CsharpFiles = workspace.CsharpFiles;
                SuggestionGroups = workspace.SuggestionGroups;
            }

            return base.PreRender();
        }

        public void ApplyFixes(SuggestionGroupData group)
        {
            ApplyFixesCore(group.Suggestions.Select(s => s.UniqueId).ToArray());
        }

        public void ApplyFix(SuggestionGroupItemData item)
        {
            ApplyFixesCore(new [] { item.UniqueId });
        }

        private void ApplyFixesCore(string[] ids)
        {
            workspace = new ConverterWorkspace();
            workspace.Initialize(OriginalMarkup, CsharpFiles);

            workspace.ApplyFixes(ids);

            needsRefresh = true;
        }
    }
}
