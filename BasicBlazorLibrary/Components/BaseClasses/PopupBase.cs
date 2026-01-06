namespace BasicBlazorLibrary.Components.BaseClasses;
public abstract class PopupBase : JavascriptComponentBase, IDisposable
{
    [Parameter]
    public bool Visible { get; set; }
    [Inject]
    private PopupRegistry? Popup { get; set; }
    [Parameter]
    public EventCallback<bool> VisibleChanged { get; set; }
    

    private CancellationTokenSource? _autoCloseCts;
    private bool _lastVisible;

    [Parameter] public int AutoCloseMilliseconds { get; set; } = 0;
    protected override void OnInitialized()
    {
        Popup?.Register(this);
        base.OnInitialized();
    }
    internal async Task RequestCloseAsync()
    {
        if (Visible == false)
        {
            return;
        }

        CancelAutoClose();
        await VisibleChanged.InvokeAsync(false);
    }

    protected override void OnParametersSet()
    {

        base.OnParametersSet();

        // Detect rising edge: false -> true
        if (Visible && _lastVisible == false)
        {
            StartAutoCloseIfNeeded();
        }
        // Detect falling edge: true -> false
        else if (Visible == false && _lastVisible)
        {
            CancelAutoClose();
        }

        _lastVisible = Visible;
    }

    private void StartAutoCloseIfNeeded()
    {
        CancelAutoClose();

        if (AutoCloseMilliseconds <= 0)
        {
            return;
        }

        _autoCloseCts = new CancellationTokenSource();
        var ct = _autoCloseCts.Token;

        _ = AutoCloseAsync(ct);
    }

    private async Task AutoCloseAsync(CancellationToken ct)
    {
        try
        {
            await Task.Delay(AutoCloseMilliseconds, ct);

            // still visible? close it.
            if (ct.IsCancellationRequested == false && Visible)
            {
                await VisibleChanged.InvokeAsync(false);
            }
        }
        catch (TaskCanceledException)
        {
            // expected
        }
    }

    private void CancelAutoClose()
    {
        try 
        {
            _autoCloseCts?.Cancel(); 
        }
        catch { /* ignore */ }
        _autoCloseCts?.Dispose();
        _autoCloseCts = null;
    }

    protected virtual async Task ClosePopupAsync()
    {
        CancelAutoClose();
        await VisibleChanged.InvokeAsync(false);
    }

    public void Dispose()
    {
        Popup?.Unregister(this);
        CancelAutoClose();
        GC.SuppressFinalize(this);
    }

    [Parameter]
    public int ZIndex { get; set; } = 1; //this is the priority.  the higher means more likely this will be on top.
}