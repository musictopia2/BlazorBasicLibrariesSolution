using BasicBlazorLibrary.Components.InputNavigations;
using Microsoft.AspNetCore.Components.Forms;
namespace BasicBlazorLibrary.Components.DataEntryHelpers;
public partial class DataEntryFormGridComponent : IDataEntryGrid
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
    public void AddHotkey(ConsoleKey key, Action action)
    {
        Key!.AddAction(key, action);
    }
    [Parameter]
    public EventCallback OnSubmit { get; set; }
    [Parameter]
    public EventCallback OnValidSubmit { get; set; }
    [Parameter]
    public EventCallback OnInvalidSubmit { get; set; }
    [Parameter]
    public object? Model { get; set; }
    [Parameter] public EditContext? EditContext { get; set; }
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