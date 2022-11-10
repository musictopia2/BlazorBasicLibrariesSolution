namespace BasicBlazorLibrary.Components.Basic;
public partial class TextAreaComponent
{
    [Parameter]
    public int Rows { get; set; } = 5;
    [Parameter]
    public bool FullPage { get; set; } = false;
    [Parameter]
    public bool UseLeftOvers { get; set; } = false;
    [Parameter]
    public string Value { get; set; } = "";
    [Parameter]
    public bool ReadOnly { get; set; }
    [Parameter]
    public EventCallback<string> ValueChanged { get; set; }
    [Parameter]
    public string Width { get; set; } = "100%";
    [Parameter]
    public string Class { get; set; } = "";
    [Parameter]
    public string Style { get; set; } = "";
    [Parameter]
    public bool SpellCheck { get; set; }
    [Parameter]
    public ConsoleKey MainHotKey { get; set; } = ConsoleKey.NoName;
    [Parameter]
    public EventCallback HotKeyPressed { get; set; }
    private ElementReference? _text;
    private KeystrokeClass? _keys;
    private bool _didInit = false;
    protected override Task OnInitializedAsync()
    {
        _didInit = false;
        _text = null;
        if (MainHotKey != ConsoleKey.NoName)
        {
            _keys = new(JS!);
            _keys.AddAction(MainHotKey, async () => await HotKeyPressed.InvokeAsync());
        }
        return base.OnInitializedAsync();
    }
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (MainHotKey == ConsoleKey.NoName)
        {
            return;
        }
        if (_didInit == false && _text is not null)
        {
            await _keys!.InitAsync(_text);
            _didInit = true;
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