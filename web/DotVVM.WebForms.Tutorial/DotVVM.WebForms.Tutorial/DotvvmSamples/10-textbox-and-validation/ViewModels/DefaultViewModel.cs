using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using DotVVM.Framework.ViewModel;
using DotVVM.Framework.ViewModel.Validation;

namespace DotVVM.WebForms.Tutorial.DotvvmSamples._10_textbox_and_validation.ViewModels
{
    public class DefaultViewModel : DotvvmViewModelBase
    {

        [Required]
        [DotvvmEnforceClientFormat]
        public DateTime BeginDate { get; set; }

        [Required]
        [DotvvmEnforceClientFormat]
        public DateTime EndDate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (BeginDate > EndDate)
            {
                yield return this.CreateValidationResult("EndDate must be greater than BeginDate!", vm => vm.BeginDate);
            }
        }

    }
}

