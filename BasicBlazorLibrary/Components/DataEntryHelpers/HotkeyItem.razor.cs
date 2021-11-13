namespace BasicBlazorLibrary.Components.DataEntryHelpers;
public partial class HotkeyItem
{
    [Parameter]
    public EventCallback Action { get; set; }
    [Parameter]
    public ConsoleKey Key { get; set; }
    [Parameter]
    public string Label { get; set; } = "";
    //hopefully no need for parameters.  well see how this goes.
    [CascadingParameter]
    private IDataEntryGrid? Grid { get; set; }
    protected override void OnInitialized()
    {
        Grid!.AddHotkey(Key, () => Action.InvokeAsync());
        base.OnInitialized();
    }
    private string FullName => $"{Label} ({Key})";
}