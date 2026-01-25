using BasicBlazorLibrary.Components.MediaQueries.MediaListUseClasses;
using aa2 = BasicBlazorLibrary.Components.CssGrids.Helpers;
namespace BasicBlazorLibrary.Components.Modals;
public partial class IntegerInputModal
{
    [Parameter]
    public int Value { get; set; }
    [Parameter]
    public EventCallback<int> ValueChanged { get; set; }
    [Parameter]
    public EnumSideLocation SideOrder { get; set; } = EnumSideLocation.Last; //default to last
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
    [Parameter]
    public bool CloseOnSubmit { get; set; } = true;

    [Parameter] 
    public bool StartBlank { get; set; } = false;

    [Parameter] public bool AllowZero { get; set; } = false;

    [Parameter]
    public string? DisplayWidth { get; set; } = "30vmin";

    [Parameter] public int? MaxValue { get; set; } // optional limit
    [Parameter]
    public int? MinValue { get; set; }
    [Parameter] public int MaxDigits { get; set; } = 2;
    [Parameter] public bool AllowNegative { get; set; } = false;

    private readonly BasicList<int> _numbers =
        [
            7, 8, 9, 4, 5, 6, 1, 2, 3
        ];
    private string _display = "";
    private void UpdateValue(int value)
    {
        UpdateValue(value.ToString());
    }
    private void UpdateValue(string value)
    {
        if (value == "0" && _display == "")
        {
            if (AllowZero == false)
            {
                return; // keep current behavior
            }

            // Allow entering 0 as the entire value (still respects MaxDigits)
            _display = "0";
            return;
        }
        if (value == "-")
        {
            if (AllowNegative == false)
            {
                return;
            }
            if (_display != "")
            {
                return; //has to ignore because a minus can't be used if not the first item.
            }
            _display = "-";
            return;
        }
        if (_display.TrimStart('-').Length < MaxDigits)
        {
            //follows that rule.
            // Temporarily build the new string
            string temp = _display + value;
            bool rets = int.TryParse(temp, out int possibleValue);
            if (rets == false)
            {
                return;
            }
            if (MaxValue.HasValue && possibleValue > MaxValue.Value)
            {
                return; //ignore because it goes over.
            }
            if (MinValue.HasValue && possibleValue < MinValue.Value)
            {
                return; //because its lower than supported value.
            }
            _display += value;
            return;
        }
    }
    private void Clear()
    {
        _display = "";
    }
    private void BackSpace()
    {
        if (_display == "")
        {
            return;
        }
        _display = _display.Substring(0, _display.Length - 1);
    }
    private void ProcessEnter()
    {
        bool rets = int.TryParse(_display, out int results);
        if (rets == true)
        {
            ValueChanged.InvokeAsync(results);
        }
        if (CloseOnSubmit == false)
        {
            return; //sometimes it can't because needs validation and if it fails would not close it.
        }
        ProcessCancel(); //this too.
    }
    private void ProcessCancel()
    {
        VisibleChanged.InvokeAsync(false); //hopefully this simple.
    }
    private static string GetRowsColumns => aa2.RepeatMinimum(4);
    private string FormattedDisplay =>
    int.TryParse(_display, out var result)
        ? result.ToString("N0") // e.g. "1,234"
        : _display;


    private bool _wasVisible;
    protected override void OnParametersSet()
    {
        // only initialize when opening
        if (Visible && _wasVisible == false)
        {
            if (StartBlank)
            {
                _display = "";
            }
            else
            {
                _display = Value.ToString();
            }
        }

        // optionally clear when closing
        if (Visible == false && _wasVisible)
        {
            _display = "";
        }

        _wasVisible = Visible;
        base.OnParametersSet();
    }
    

}