using BasicBlazorLibrary.Components.AutoCompleteHelpers;
using BasicBlazorLibrary.Components.ComboTextboxes;
using BasicBlazorLibrary.Components.InputNavigations;
namespace BasicBlazorLibrary.Components.Layouts;
public partial class FullComboGenericLayout<TValue> : IFocusInput
{
    public int TabIndex { get; set; } = -1;
    [CascadingParameter]
    public InputTabOrderNavigationContainer? TabContainer { get; set; }
    public Task LoseFocusAsync() //if no forms are involved, should never have to do anything for losing focus.
    {
        return Task.CompletedTask;
    }
    private string GetHeaderFontSize => HeaderFontSize == "" ? Style.FontSize : HeaderFontSize;
    [Parameter]
    public string Title { get; set; } = "";
    [Parameter]
    public string HeaderFontSize { get; set; } = "";
    [Parameter]
    public BasicList<TValue> ItemList { get; set; } = new();
    [Parameter]
    public bool FirstFocus { get; set; } = true;
    [Parameter]
    public TValue? Value { get; set; }
    [Parameter]
    public EventCallback<TValue> ValueChanged { get; set; }
    [Parameter]
    public Func<TValue, string>? RetrieveValue { get; set; }
    [Parameter]
    public EventCallback ComboEnterPressed { get; set; }
    [Parameter]
    public bool IsChild { get; set; }
    [Parameter]
    public string ManuelHeight { get; set; } = "";
    [Parameter]
    public AutoCompleteStyleModel Style { get; set; } = new();
    [Parameter]
    public bool Virtualized { get; set; }
    private string GetRows => $"auto {ManuelHeight}";
    private ComboBoxGenericList<TValue>? _comboBox;
    private bool _didFocus;
    private KeystrokeClass? _keyClass;
    protected override void OnInitialized()
    {
        Style.Height = "100%";
        Style.Width = "100%";
        _comboBox = null;
        if (TabContainer is not null)
        {
            _keyClass = new(TabContainer.JS!);
            _keyClass.AddShiftTab(ProcessShiftTab);
            _keyClass.AddAction(ConsoleKey.Tab, ProcessEnter);
            _keyClass.AddAction(ConsoleKey.Enter, ProcessEnter);
            TabContainer.AddFocusItem(this);
        }
    }
    protected virtual async void ProcessEnter()
    {
        await LoseFocusAsync();
        if (ComboEnterPressed.HasDelegate)
        {
            return;
        }
        await TabContainer!.FocusNextAsync();
    }
    private async void ProcessShiftTab()
    {
        await LoseFocusAsync();
        await TabContainer!.FocusPreviousAsync();
    }
    private void PrivateChange(TValue value)
    {
        ValueChanged.InvokeAsync(value);
    }
    public async ValueTask FocusAsync()
    {
        await PrivateFocusAsync();
    }
    private async Task PrivateFocusAsync()
    {
        if (_comboBox is null)
        {
            return;
        }
        if (_comboBox.TextReference is null)
        {
            return;
        }
        _didFocus = true;
        await _comboBox.TextReference.Value.FocusAsync();
    }
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender && _keyClass is not null)
        {
            await _keyClass.InitAsync(_comboBox!.TextReference);
        }
        if (FirstFocus == false || _didFocus)
        {
            return;
        }
        await FocusAsync();
    }
    async Task IFocusInput.FocusAsync()
    {
        await Task.Delay(50);
        if (TabContainer is not null)
        {
            await TabContainer.FocusAndSelectAsync(_comboBox!.TextReference);
        }
        else
        {
            await PrivateFocusAsync();
        }
    }
}