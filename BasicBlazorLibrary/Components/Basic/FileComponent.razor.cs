namespace BasicBlazorLibrary.Components.Basic;
public partial class FileComponent
{
    [Parameter]
    public UploadFileInfo? FileInfo { get; set; }
    [Parameter]
    public EventCallback<IBrowserFile> OnFileSelected { get; set; }
    [Parameter]
    public int MaxFiles { get; set; } = 1;
    [Parameter]
    public EventCallback<IReadOnlyList<IBrowserFile>> OnFilesSelected { get; set; }
    [Parameter]
    public string? Style { get; set; } // Inline style support

    [Parameter]
    public string? CssClass { get; set; } // CSS class support

    private string _errorMessage = "";
    private string AcceptString => FileInfo != null && FileInfo.AllowedContentTypes.Count != 0
            ? string.Join(",", FileInfo.AllowedContentTypes)
            : string.Empty;
    private async Task HandleChange(InputFileChangeEventArgs e)
    {
        if (MaxFiles == 1)
        {
            var singleFile = e.File;
            if (IsValid(singleFile))
            {
                await OnFileSelected.InvokeAsync(singleFile);
            }
            return;
        }
        var files = e.GetMultipleFiles(MaxFiles);
        if (files is null)
        {
            return;    
        }
        if (files.Any() == false)
        {
            return;
        }
        foreach (var file in files)
        {
            if (IsValid(file) == false)
            {
                return;
            }
        }
        await OnFilesSelected.InvokeAsync(files);
    }
    private bool IsValid(IBrowserFile file)
    {
        if (FileInfo is null)
        {
            _errorMessage = "No File Info";
            return false;
        }
        if (FileInfo.AllowedContentTypes.Contains(file.ContentType) == false)
        {
            _errorMessage = $"Invalid file type in one of the files. Allowed: {string.Join(", ", FileInfo.AllowedContentTypes)}";
            return false;
        }
        if (file.Size > FileInfo.MaxSize)
        {
            _errorMessage = $"One of the files exceeds max size of {FileInfo.MaxSize / 1_000_000} MB.";
            return false;
        }
        _errorMessage = "";
        return true;
    }
    public void ClearError() => _errorMessage = "";
}