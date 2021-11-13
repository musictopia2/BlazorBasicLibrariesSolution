namespace BasicBlazorLibrary.Components.BaseClasses;
public abstract class HotkeyComponentBase : JavascriptComponentBase
{
    //this needs the javascript so if anything else needs javascript, might as well have that as well.
    /// <summary>
    /// this is the element used for the hotkey system.
    /// </summary>
    protected ElementReference? MainElement { get; set; }
    protected KeystrokeClass? Key;
    protected virtual bool FocusOnFirst { get; set; }
    /// <summary>
    /// this is needed because you could have a base class that has to inherit from the hotkey class but its sub class may not need any.
    /// </summary>
    protected virtual bool NeedsHotKeys { get; } = true;
    protected async Task InitAsync()
    {
        if (MainElement.HasValue == false)
        {
            return;
        }
        _didInit = true;
        await Key!.InitAsync(MainElement);
        if (FocusOnFirst)
        {
            await MainElement.Value.FocusAsync();
        }
    }
    protected override void OnInitialized()
    {
        if (NeedsHotKeys == false)
        {
            return;
        }
        MainElement = null;

        Key = new KeystrokeClass(JS!);
        base.OnInitialized();
    }
    private bool _didInit;
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (NeedsHotKeys == false)
        {
            return;
        }
        if (_didInit == false)
        {
            await InitAsync();
        }
    }
}