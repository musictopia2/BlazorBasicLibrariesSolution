using CommonBasicLibraries.BasicUIProcesses;
namespace BasicBlazorLibrary.Components.Inputs;
public partial class InputEnterDate<TValue>
{
    private string _value = "";
    private DateOnly? _dateChosen;
    private TextBoxHelperClass? _helps;
    [Inject]
    private IToast? Toast { get; set; }
    protected override async Task<bool> CustomValid()
    {
        _value = await _helps!.GetValueAsync(InputElement);
        if (_value == "")
        {
            CurrentValue = default;
            return true;
        }
        bool rets = _value.IsValidDate(out DateOnly? date);
        if (rets == false)
        {
            Toast!.ShowUserErrorToast("Invalid Date");
            CurrentValue = default;
            await ClearTextAsync();
            return false;
        }
        _dateChosen = date;
        object temps = _dateChosen!;
        try
        {
            CurrentValue = (TValue)temps;
        }
        catch (Exception)
        {
            temps = _dateChosen!.Value.ToDateTime();
            CurrentValue = (TValue)temps;
        }

        _previousValue = CurrentValue;
        return true;
    }
    //public override async Task LoseFocusAsync()
    //{
    //    _value = await _helps!.GetValueAsync(InputElement);
    //    if (_value == "")
    //    {
    //        CurrentValue = default;
    //        return;
    //    }
    //    bool rets = _value.IsValidDate(out DateOnly? date);
    //    if (rets == false)
    //    {
    //        Toast!.ShowUserErrorToast("Invalid Date");
    //        CurrentValue = default;
    //        await ClearTextAsync();
    //        return;
    //    }
    //    _dateChosen = date;
    //    object temps = _dateChosen!;
    //    try
    //    {
    //        CurrentValue = (TValue)temps;
    //    }
    //    catch (Exception)
    //    {
    //        temps = _dateChosen!.Value.ToDateTime();
    //        CurrentValue = (TValue)temps;
    //    }

    //    _previousValue = CurrentValue;
    //}
    private bool _invalid;
    private static string GetFormattedDate(DateOnly date)
    {
        string dayString = date.Day.ToString("00");
        string monthString = date.Month.ToString("00");
        string yearString = date.Year.ToString("00");
        return $"{monthString}{dayString}{yearString}";
    }
    protected override void OnInitialized()
    {
        base.OnInitialized();
        _helps = new TextBoxHelperClass(TabContainer.JS!);
        KeyStrokeHelper.AddAction(ConsoleKey.C, () =>
        {
            if (_dateChosen.HasValue == false)
            {
                _dateChosen = DateOnly.FromDateTime(DateTime.Now);
            }
            _value = "";
            TabContainer.OtherScreen = true;
            StateHasChanged();
        });
    }
    private async Task Cancelled()
    {
        _dateChosen = null;
        await ClearTextAsync();
        TabContainer.OtherScreen = false;
        await TabContainer.FocusSpecificInputAsync(this);
    }
    private async Task ChoseDate()
    {
        DateOnly date = _dateChosen!.Value;
        _value = GetFormattedDate(date);
        await _helps!.SetNewValueAloneAsync(InputElement, _value);
        TabContainer.OtherScreen = false;
        ProcessEnter();
    }
    private TValue? _previousValue;
    protected override void OnParametersSet()
    {
        if (TabContainer.OtherScreen)
        {
            return;
        }
        if (_previousValue!.Equals(Value))
        {
            return;
        }
        _previousValue = Value;
        if (Value == null || Value.Equals(default))
        {
            _dateChosen = null;
            _invalid = false;
        }
        else if (Value is DateOnly aa)
        {
            _invalid = false;
            if (aa.Equals(default))
            {
                _dateChosen = null;
            }
            else
            {
                _dateChosen = aa;
            }
        }
        else if (Value is DateTime ab)
        {
            _invalid = false;
            if (ab.Equals(default))
            {
                _dateChosen = null;
            }
            else
            {
                _dateChosen = ab.ToDateOnly();
            }
        }
        else
        {
            _dateChosen = null;
            _invalid = true;
        }
        if (_dateChosen == null)
        {
            _value = "";
        }
        else
        {
            _value = GetFormattedDate(_dateChosen.Value);
        }
        base.OnParametersSet();
    }
    private async Task ClearTextAsync()
    {
        await _helps!.ClearTextAsync(InputElement);
        _value = "";
    }
    protected override async Task OnFirstRenderAsync()
    {
        await _helps!.SetInitTextAsync(InputElement, _value);
    }
}