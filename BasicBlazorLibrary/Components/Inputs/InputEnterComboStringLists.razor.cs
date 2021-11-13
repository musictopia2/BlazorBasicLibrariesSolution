using BasicBlazorLibrary.Components.AutoCompleteHelpers;
using BasicBlazorLibrary.Components.ComboTextboxes;
namespace BasicBlazorLibrary.Components.Inputs;
public partial class InputEnterComboStringLists
{
    private ComboBoxStringList? _combo;
    protected override void OnInitialized()
    {
        _value = CurrentValue;
        _combo = null;
        base.OnInitialized();
    }
    protected override void OnParametersSet()
    {
        base.OnParametersSet();
    }
    protected override void AfterCurrentChanged()
    {
        _value = CurrentValue;
    }
    protected override Task OnFirstRenderAsync()
    {
        InputElement = _combo!.GetTextBox;
        _combo.ElementFocused = () =>
        {
            TabContainer.ResetFocus(this);
        };
        return Task.CompletedTask;
    }
    public override Task LoseFocusAsync()
    {
        if (_value != "" && RequiredFromList && ItemList.Any(xxx => xxx == _value) == false)
        {
            _value = "";
        }
        CurrentValue = _value;
        return Task.CompletedTask;
    }
    [Parameter]
    public BasicList<string> ItemList { get; set; } = new();
    [Parameter]
    public bool RequiredFromList { get; set; } = true;
    [Parameter]
    public AutoCompleteStyleModel Style { get; set; } = new AutoCompleteStyleModel();
    [Parameter]
    public bool Virtualized { get; set; } = false;
    [Parameter]
    public EventCallback ComboEnterPressed { get; set; }
    private string _value = "";
}