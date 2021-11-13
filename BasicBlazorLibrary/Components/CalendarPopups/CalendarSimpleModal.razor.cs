using CommonBasicLibraries.BasicUIProcesses;
using aa = BasicBlazorLibrary.Components.CssGrids.Helpers;
namespace BasicBlazorLibrary.Components.CalendarPopups;
public partial class CalendarSimpleModal<TValue>
{
    [Inject]
    private IToast? Toast { get; set; }
    //this time, can't inherit from the base popup class because i need hotkey features.
    [Parameter]
    public EventCallback Cancelled { get; set; }
    [Parameter]
    public EventCallback ChoseDate { get; set; }
    private static string GetColumns() => aa.RepeatSpreadOut(7);
    private static string GetRows() => aa.RepeatSpreadOut(8);
    [Parameter]
    public TValue? DateToDisplay { get; set; }
    private DateOnly? _todisplay;
    [Parameter]
    public EventCallback<TValue> DateToDisplayChanged { get; set; }
    private string GetColorStyle(DateOnly date)
    {
        if (_todisplay.HasValue)
        {
            if (_todisplay.Value.Day == date.Day)
            {
                return $"background-color: {cc.Aqua.ToWebColor()};";
            }
        }
        return "";
    }
    private string _monthLabel = "";
    private readonly BasicList<string> _dayList = new()
    {
        DayOfWeek.Sunday.DayOfWeekShort(),
        DayOfWeek.Monday.DayOfWeekShort(),
        DayOfWeek.Tuesday.DayOfWeekShort(),
        DayOfWeek.Wednesday.DayOfWeekShort(),
        DayOfWeek.Thursday.DayOfWeekShort(),
        DayOfWeek.Friday.DayOfWeekShort(),
        DayOfWeek.Saturday.DayOfWeekShort()
    };
    private ElementReference? _text;
    private string _realValue = "";
    protected override void OnInitialized()
    {
        _text = null;
        base.OnInitialized();
#pragma warning disable CA2012 // Use ValueTasks correctly
        Key!.AddAction(ConsoleKey.F1, () => _text!.Value.FocusAsync());
#pragma warning restore CA2012 // Use ValueTasks correctly
        //somehow worked properly for my case though.
        Key.AddAction(ConsoleKey.F2, () => DayClicked(DateOnly.FromDateTime(DateTime.Now)));
        Key.AddAction(ConsoleKey.C, () => ClearText());
        Key.AddAction(ConsoleKey.X, () => Cancelled.InvokeAsync());
        Key.AddArrowUpAction(() =>
        {
            _todisplay = _todisplay!.Value.AddDays(-7);
            DayClicked(_todisplay.Value);
        });
        Key.AddArrowDownAction(() =>
        {
            _todisplay = _todisplay!.Value.AddDays(7);
            DayClicked(_todisplay.Value);
        });
        Key.AddAction(ConsoleKey.LeftArrow, () =>
        {
            _todisplay = _todisplay!.Value.AddDays(-1);
            DayClicked(_todisplay.Value);
        });
        Key.AddAction(ConsoleKey.RightArrow, () =>
        {
            _todisplay = _todisplay!.Value.AddDays(1);
            DayClicked(_todisplay.Value);
        });
        Key.AddAction(ConsoleKey.Enter, () =>
        {
            if (_realValue == "")
            {
                object temps = _todisplay!;
                DateOnly? selectedDate = (DateOnly?)temps;
                if (selectedDate.HasValue == false)
                {
                    Toast!.ShowInfoToast("There was no date.  Rethink"); //needs toasts afterall.
                    return;
                }
                DayClicked(_todisplay!.Value);
                ChoseDate.InvokeAsync();
                return;
            }
            bool rets = _realValue.IsValidDate(out DateOnly? date);
            if (rets == false)
            {
                Toast!.ShowUserErrorToast("Invalid Date");
                ClearText();
                return;
            }
            DayClicked(date!.Value);
        });
    }
    protected override bool ShouldRender()
    {
        return DateToDisplay != null;
    }
    private async void ClearText()
    {
        await Task.Delay(10);
        _realValue = "";
        StateHasChanged();
    }
    private bool _invalid;
    protected override void OnParametersSet()
    {
        if (_todisplay.Equals(DateToDisplay))
        {
            return;
        }
        if (DateToDisplay == null)
        {
            return;
        }
        if (DateToDisplay is DateOnly temps)
        {
            _todisplay = temps;
        }
        else
        {
            _invalid = true;
            _todisplay = null;
            return;
        }
        _realValue = "";
        _monthLabel = _todisplay.Value.FirstDayStringMonth();
        DateOnly start = _todisplay.Value.FirstDayOfMonth();
        DateOnly end = _todisplay.Value.LastDayOfMonth();
        int howMany = _todisplay.Value.DaysInMonth();
        _dates = new();
        DateOnly current = start;
        howMany.Times(x =>
        {
            _dates.Add(GetSpot(current, start, howMany));
            current = current.AddDays(1);
        });
        _needsFocus = true;
        base.OnParametersSet();
    }
    private static DateSpot GetSpot(DateOnly date, DateOnly start, int howMany)
    {
        DateSpot output = new();
        output.Column = date.DayOfWeek.DayOfWeekColumn();
        output.Date = date;
        int row = 3;
        int upto = 0;
        DateOnly dateAt = start;
        do
        {
            upto++;
            if (upto > howMany)
            {
                throw new CustomBasicException("To the end for dates.  Rethink");
            }
            if (dateAt.Equals(date))
            {
                output.Row = row;
                return output;
            }
            dateAt = dateAt.AddDays(1);
            if (dateAt.DayOfWeek == DayOfWeek.Sunday)
            {
                row++;
            }

        } while (true);
    }
    private void DayClicked(DateOnly day)
    {
        object ourvalue = day;
        DateToDisplayChanged.InvokeAsync((TValue?)ourvalue);
    }
    private bool _ran;
    private BasicList<DateSpot> _dates = new();
    private bool _needsFocus = true;
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (MainElement == null)
        {
            return;
        }
        if (_ran == false)
        {
            await InitAsync();
        }
        if (_needsFocus)
        {
            await _text!.Value.FocusAsync();
            _needsFocus = false;
        }
        _ran = true;
    }
}