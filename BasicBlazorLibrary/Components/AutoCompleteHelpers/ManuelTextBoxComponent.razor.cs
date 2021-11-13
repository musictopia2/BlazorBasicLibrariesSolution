namespace BasicBlazorLibrary.Components.AutoCompleteHelpers;
public partial class ManuelTextBoxComponent
{
    private TextBoxHelperClass? _helps;
    public ElementReference? Text;
    [Parameter]
    public AutoCompleteStyleModel? Style { get; set; } = new();
    [Parameter]
    public int TabIndex { get; set; } = -1;
    [Parameter]
    public string Placeholder { get; set; } = "";
    private string GetTextStyle()
    {
        return $"font-size: {Style!.FontSize}; color: {Style.HeaderTextColor};";
    }
    private string GetTextBackgroundColor => Style!.HeaderBackgroundColor == cc.Transparent.ToWebColor() ? "inherit" : Style.HeaderBackgroundColor;
    public async Task<string> GetValueAsync()
    {
        return await _helps!.GetValueAsync(Text);
    }
    public Action<TextModel>? KeyPress { get; set; }
    private async void PrivateKeyPress(string key)
    {
        string value = await _helps!.GetValueAsync(Text);
        if (key != "Enter")
        {
            value = $"{value}{key}";
        }
        TextModel text = new(key, value);
        KeyPress?.Invoke(text);
    }
    protected override void OnInitialized()
    {
        Text = null;
        _helps = new TextBoxHelperClass(JS!);
        _helps.OnKeyPress += PrivateKeyPress;
        base.OnInitialized();
    }
    public async Task SetInitValueAsync(string value)
    {
        await _helps!.SetInitTextAsync(Text, value);
    }
    public async Task ClearAsync()
    {
        await _helps!.ClearTextAsync(Text);
    }
    public async Task HighlightTextAsync(string value, int startAt)
    {
        await _helps!.SetNewValueAndHighlightAsync(Text, value, startAt);
    }
    public async Task SetTextValueAloneAsync(string value)
    {
        await _helps!.SetNewValueAloneAsync(Text, value);
    }
    protected override bool ShouldRender()
    {
        return false;
    }
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await _helps!.StartAsync(Text);
        }

    }
}