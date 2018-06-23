## FileUpload

-------------------------------------

In ASP.NET, files can be uploaded on a postback. It is not easy to show the upload progress to the user without a third-party control.

### Default.aspx

```DOTHTML
<asp:FileUpload ID="ImageFileUpload" runat="server" />

<asp:Button ID="UploadButton" runat="server" 
            Text="Store Image"
            OnClick="UploadButton_OnClick"/>
```

### Default.aspx.cs

```CSHARP
protected void UploadButton_OnClick(object sender, EventArgs e)
{
    if (ImageFileUpload.HasFiles)
    {
        foreach (var file in ImageFileUpload.PostedFiles)
        {
            file.SaveAs("filename.png");
        }
    }
}
```

-------------------------------------

DotVVM [FileUpload](https://www.dotvvm.com/docs/controls/builtin/FileUpload/2.0) control can display upload progress, errors, and can be styled using CSS. It always uploads files on the background. 

The files are saved in a temporary store on the server and can be retrieved by their unique IDs stored in the viewmodel.

### Default.dothtml

```DOTHTML
<dot:FileUpload UploadedFiles="{value: Uploads}" 
                UploadCompleted="{command: StoreFile()}" />
```

### DefaultViewModel.cs

```CSHARP
// use dependency injection to request uploaded file storage
private readonly IUploadedFileStorage uploadedFileStorage;

public DefaultViewModel(IUploadedFileStorage uploadedFileStorage)
{
    this.uploadedFileStorage = uploadedFileStorage;
}

// the file IDs are kept in this collection
public UploadedFilesCollection Uploads { get; set; } = new UploadedFilesCollection();

public void StoreFile()
{
    foreach (var file in Uploads.Files)
    {
        uploadedFileStorage.SaveAs(file.FileId, "filename.png");
    }
}
```