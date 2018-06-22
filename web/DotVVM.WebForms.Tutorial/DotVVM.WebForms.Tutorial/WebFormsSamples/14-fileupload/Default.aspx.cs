using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DotVVM.WebForms.Tutorial.WebFormsSamples._14_fileupload
{
    public partial class Default : System.Web.UI.Page
    {

        protected void UploadButton_OnClick(object sender, EventArgs e)
        {
            if (ImageFileUpload.HasFiles)
            {
                foreach (var file in ImageFileUpload.PostedFiles)
                {
                    //file.SaveAs("filename.png");
                }
            }
        }
    }
}