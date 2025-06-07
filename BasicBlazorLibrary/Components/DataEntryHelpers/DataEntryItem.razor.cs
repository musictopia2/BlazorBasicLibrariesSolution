namespace BasicBlazorLibrary.Components.DataEntryHelpers;
public partial class DataEntryItem : IDisposable
{
    private bool _disposedValue;
    [Parameter]
    public string Header { get; set; } = "";
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
    [Parameter]
    public string Style { get; set; } = "";

    protected override void OnInitialized()
    {
        base.OnInitialized();
    }
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {

            }
            _disposedValue = true;
        }
    }
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}