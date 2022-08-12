namespace BasicBlazorLibrary.Components.Inputs;
public partial class InputEnterTextArea
{
    [Parameter]
    public int Rows { get; set; } = 5;
    [Parameter]
    public bool FullPage { get; set; } = false;
    [Parameter]
    public bool ReadOnly { get; set; }
    [Parameter]
    public bool UseLeftOvers { get; set; } = false;
    [Parameter]
    public string Width { get; set; } = "100%";
    [Parameter]
    public ConsoleKey HotKeyForSubmit { get; set; } = ConsoleKey.NoName;
    protected override bool AllowEnter => false;
    protected override void AddOtherHotKeys()
    {
        if (HotKeyForSubmit != ConsoleKey.NoName)
        {
            KeyStrokeHelper.AddAction(HotKeyForSubmit, async () => await Form!.HandleSubmitAsync());
        }
    }
    private async Task HandleOnChange(ChangeEventArgs args)
    {
        if (args is null || args.Value is null)
        {
            await ValueChanged!.InvokeAsync(null);
            return;
        }
        string data = args.Value.ToString()!;
        await ValueChanged.InvokeAsync(data);
    }
}