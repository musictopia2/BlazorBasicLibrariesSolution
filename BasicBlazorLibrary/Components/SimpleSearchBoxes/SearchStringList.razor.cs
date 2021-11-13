using BasicBlazorLibrary.Components.AutoCompleteHelpers;
namespace BasicBlazorLibrary.Components.SimpleSearchBoxes;
public partial class SearchStringList : IAsyncDisposable
{
    private AutoCompleteService? _service;
    [Parameter]
    public BasicList<string>? ItemList { get; set; }
    private BasicList<string> _displayList = new();
    private string _value = "";
    [Parameter]
    public string Value
    {
        get => _value;
        set
        {
            if (_value != value)
            {
                _value = value;
                UpdateValuesAsync();
            }
        }
    }
    [Parameter]
    public EventCallback<string> ValueChanged { get; set; }
    [Parameter]
    public EventCallback SearchEnterPressed { get; set; }
    [Parameter]
    public bool RequiredFromList { get; set; } = true;
    [Parameter]
    public AutoCompleteStyleModel Style { get; set; } = new AutoCompleteStyleModel();
    [Parameter]
    public int TabIndex { get; set; } = -1;
    [Parameter]
    public string Placeholder { get; set; } = "";
    [Parameter]
    public bool RequiredTab { get; set; } = false;
    private string _firstText = "";
    private ManuelTextBoxComponent? _text;
    public ElementReference? GetTextBox => _text!.Text;
    private ElementReference? _scrollreference;
    private ElementReference? _firstreference;
    private void PrivateUpdate(string value, bool laternewValue)
    {
        if (value == Value)
        {
            return;
        }
        Value = value;
        if (laternewValue)
        {
            return;
        }
        ValueChanged.InvokeAsync(value);
    }
    protected override void OnInitialized()
    {
        _text = null;
        _service = new AutoCompleteService(JS!);
        _service.ArrowDown += ArrowDown;
        _service.ArrowUp += ArrowUp;
        _service.BackspacePressed += BackspacePressed;
        _scrollreference = null;
        _firstreference = null;
        _displayList = ItemList!.ToBasicList();
        _service.Update(_displayList.Count);
        base.OnInitialized();
    }
    private async void ArrowUp()
    {
        _service!.MoveUp();
        PrivateUpdate(_displayList![_service.ElementHighlighted], false);
        _firstText = Value;
        await _text!.SetTextValueAloneAsync(Value);
        await ContinueArrowProcessesAsync();
    }
    private async void ArrowDown()
    {
        _service!.MoveDown();
        PrivateUpdate(_displayList![_service.ElementHighlighted], false);
        _firstText = Value;
        await _text!.SetTextValueAloneAsync(Value);
        await ContinueArrowProcessesAsync();
    }
    private async void BackspacePressed()
    {
        _firstText = "";
        await _text!.ClearAsync();
        _displayList = ItemList!.ToBasicList();
        PrivateUpdate(_firstText, false);
        StateHasChanged();
    }
    private async Task ContinueArrowProcessesAsync()
    {
        StateHasChanged();
        await Task.Delay(10);
        PrivateUpdate(_displayList![_service!.ElementHighlighted], false);
        _firstText = Value;
    }
    private async Task ElementDoubleClicked()
    {
        if (Style.AllowDoubleClick == false)
        {
            return;
        }
        await SearchEnterPressed.InvokeAsync();
    }
    public Action? ElementFocused { get; set; }
    private async Task ElementClicked(int x)
    {
        _service!.DoHighlight(x, false);
        PrivateUpdate(_displayList![_service.ElementHighlighted], false);
        _firstText = Value;
        await _text!.SetTextValueAloneAsync(Value);
        await _text.Text!.Value.FocusAsync();
        ElementFocused?.Invoke();
    }
    private string GetTextStyle()
    {
        return $"font-size: {Style!.FontSize}; color: {Style.ComboTextColor};";
    }
    private string GetHoverColor(int id)
    {
        if (id != _service!.ElementHighlighted)
        {
            return Style.HoverColor;
        }
        return Style.HighlightColor;
    }
    private string GetBackgroundColor(int id)
    {
        if (id != _service!.ElementHighlighted)
        {
            return Style.ComboBackgroundColor;
        }
        return Style.HighlightColor;
    }
    private string GetColorStyle(int id)
    {
        if (id != _service!.ElementHighlighted)
        {
            return "";
        }
        return $"background-color: {Style.HighlightColor};";
    }
    private async void UpdateValuesAsync()
    {
        if (_didFirst)
        {
            string value = await _text!.GetValueAsync();
            if (value == "" && Value != "" || value != Value)
            {
                await _text.SetInitValueAsync(Value);
                var index = _displayList!.IndexOf(Value);
                _service!.DoHighlight(index, true);
                _firstText = Value;
            }
            else if (Value == "" && value != "")
            {
                _firstText = "";
                await _text!.ClearAsync();
                _service!.Unhighlight(true);
                PrivateUpdate("", false);
            }
        }
        else if (Value != "")
        {
            var index = _displayList!.IndexOf(Value);
            _service!.DoHighlight(index, true);
            _firstText = Value;
        }
    }
    private async void OnKeyPress(TextModel model)
    {
        if (model.KeyPressed == "Enter")
        {
            if (SearchEnterPressed.HasDelegate == false)
            {
                return;
            }
            string realValue = model.Value;
            if (RequiredFromList)
            {
                if (_service!.ElementScrollTo == -1 && _service.ElementHighlighted == -1)
                {
                    _service.DoHighlight(0, true);
                    realValue = _displayList.First();
                    PrivateUpdate(realValue, false);
                    await _text!.SetTextValueAloneAsync(realValue);
                    StateHasChanged();
                }
            }
            if (ValueChanged.HasDelegate)
            {
                await ValueChanged.InvokeAsync(realValue);
                await SearchEnterPressed.InvokeAsync();
            }
            return;
        }
        _firstText += model.KeyPressed;

        _displayList = ItemList!.Where(xxx => xxx.ToLower().Contains(_firstText.ToLower())).ToBasicList();
        _service!.Update(_displayList.Count);
        await _text!.SetTextValueAloneAsync(_firstText);
        _service.Unhighlight(true);
        StateHasChanged();
    }
    private bool _didFirst = false;
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _didFirst = true;
            await _text!.SetInitValueAsync(Value);
            _text.KeyPress = OnKeyPress;
            await _service!.InitializeAsync(_text!.Text);
        }
        if (_service!.NeedsToScroll && _service.ElementScrollTo == -1)
        {
            await _service.ScrollToElementAsync(_firstreference);
        }
        if (_service.NeedsToScroll && _scrollreference != null)
        {
            await _service.ScrollToElementAsync(_scrollreference);
            _scrollreference = null;
        }
    }
    ValueTask IAsyncDisposable.DisposeAsync()
    {
        if (_service == null)
        {
            return ValueTask.CompletedTask;
        }
        _service.ArrowDown -= ArrowDown;
        _service.ArrowUp -= ArrowUp;
        _service.BackspacePressed -= BackspacePressed;
        return ValueTask.CompletedTask;
    }
}