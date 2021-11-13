using BasicBlazorLibrary.Components.InputNavigations;
namespace BasicBlazorLibrary.Components.DataEntryHelpers;
public partial class DataEntryFullPageComponent
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
    public void AddHotkey(ConsoleKey key, Action action)
    {
        Key!.AddAction(key, action);
    }
    protected override void OnInitialized()
    {
        _tabs = null;
        base.OnInitialized();
    }
    private InputTabOrderNavigationContainer? _tabs;
    public async Task FocusFirstAsync()
    {
        await _tabs!.FocusFirstAsync();
    }
}