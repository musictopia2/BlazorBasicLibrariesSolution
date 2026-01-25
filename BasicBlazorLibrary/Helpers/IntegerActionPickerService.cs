namespace BasicBlazorLibrary.Helpers;
public class IntegerActionPickerService
{
    public bool Visible { get; private set; }
    public int? MaxValue { get; private set; }

    // Optional: consumer supplies side content (normal Razor fragment)
    public RenderFragment? SideContent { get; private set; }

    // Returns true => close modal, false => keep open
    public Func<int, Task<bool>>? CompletedAsync { get; set; }

    public Action? StateChanged { get; set; }

    public void Pick(int? maxValue, RenderFragment? sideContent = null)
    {
        MaxValue = maxValue;
        SideContent = sideContent;
        Visible = true;
        StateChanged?.Invoke();
    }

    public async Task SubmitAsync(int value)
    {
        var handler = CompletedAsync;
        if (handler is null)
        {
            Reset();
            return;
        }

        bool close = await handler(value);

        if (close)
        {
            Reset();   
        }

        StateChanged?.Invoke();
    }
    private void Reset()
    {
        Visible = false;
        SideContent = null;
        MaxValue = null;
        CompletedAsync = null;
    }
    public void UpdateVisibleStatus(bool visible)
    {
        if (visible == false)
        {
            Reset();
        }
        else
        {
            Visible = true;
        }
        StateChanged?.Invoke();
    }
}