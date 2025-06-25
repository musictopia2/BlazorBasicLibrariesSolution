using aa2 = BasicBlazorLibrary.Components.CssGrids.Helpers;
namespace BasicBlazorLibrary.Components.NumericMobileHelpers;
public partial class WholeCurrencyPopup
{
    [Parameter]
    public decimal Value { get; set; }
    [Parameter]
    public EventCallback<decimal> ValueChanged { get; set; }
    private string _display = "";
    private static string GetRowsColumns => aa2.RepeatMinimum(4);
    private readonly BasicList<int> _numbers = new()
    {
        7,
        8,
        9,
        4,
        5,
        6,
        1,
        2,
        3
    };
    protected override void OnParametersSet()
    {
        if (Visible == false || Value == 0)
        {
            _display = "";
            return;
        }
        _display = Value.ToString();
        base.OnParametersSet();
    }
    private void UpdateValue(int value)
    {
        UpdateValue(value.ToString());
    }
    private void UpdateValue(string value)
    {
        if (value == "0")
        {
            if (_display == "")
            {
                return;
            }
        }
        _display += value;
    }
    private void ProcessEnter()
    {
        bool rets = decimal.TryParse(_display, out decimal results);
        if (rets == true)
        {
            ValueChanged.InvokeAsync(results);
        }
        ProcessCancel();
    }
    private void ProcessCancel()
    {
        VisibleChanged.InvokeAsync(false);
    }
    private void Clear()
    {
        _display = "";
    }
    private void Extra0s(int howMany)
    {
        UpdateValue(0);
        howMany.Times(x =>
        {
            UpdateValue(0);
        });
    }
    private string GetDisplay()
    {
        bool rets = decimal.TryParse(_display, out decimal results);
        if (rets == false)
        {
            return "";
        }
        return results.ToCurrency(0, false);
    }
    private void BackSpace()
    {
        if (_display == "")
        {
            return;
        }
        if (_display.EndsWith("0") == false)
        {
            _display = _display.Substring(0, _display.Length - 1);
        }
        _display = _display.BackSpaceRemoveEnding0s();
    }
}