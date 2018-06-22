using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Storage;
using DotVVM.Framework.ViewModel;

namespace DotVVM.WebForms.Tutorial.DotvvmSamples._14_fileupload.ViewModels
{
    public class DefaultViewModel : DotvvmViewModelBase
    {
        private readonly IUploadedFileStorage uploadedFileStorage;

        public DefaultViewModel(IUploadedFileStorage uploadedFileStorage)
        {
            this.uploadedFileStorage = uploadedFileStorage;
        }

        public UploadedFilesCollection Uploads { get; set; } = new UploadedFilesCollection();

        public void StoreFile()
        {
            foreach (var file in Uploads.Files)
            {
                // uploadedFileStorage.SaveAs(file.FileId, "filename.png");
            }
        }
    }
}

