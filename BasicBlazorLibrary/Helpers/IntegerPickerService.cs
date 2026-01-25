namespace BasicBlazorLibrary.Helpers;
public sealed class IntegerPickerService
{
    public Action? StateChanged;

    public Action<int>? Completed;

    public bool Visible { get; private set; }
    public int? MaxValue { get; private set; }
    public int? MinValue { get; private set; } // optional if you decide to use it later

    public void Pick(int? maxValue = null, int? minValue = null)
    {
        MaxValue = maxValue;
        MinValue = minValue;
        Visible = true;
        StateChanged?.Invoke(); //let them know something changed.

    }


    public void Submit(int value)
    {
        Completed?.Invoke(value);
    }

    public void UpdateVisibleStatus(bool currentValue)
    {
        if (currentValue == true)
        {
            return; //so if it returns its visible, then 
        }
        if (Visible == false)
        {
            return;
        }
        Visible = false;
        StateChanged?.Invoke();
    }
}