using System.Reflection.Metadata;
using aa1 = BasicBlazorLibrary.Components.CssGrids.Helpers;
namespace BasicBlazorLibrary.Components.Basic;
public partial class MultiselectCheckboxComponent<TValue>
{
    [Parameter]
    public string CheckBoxedImage { get; set; } = "";
    [Parameter]
    public string UncheckedImage { get; set; } = "";
    [Parameter]
    public bool Mobile { get; set; }
    private static string GetColumns => aa1.RepeatMaximum(2);
    [Parameter]
    [EditorRequired]
    public BasicList<TValue> Data { get; set; } = new();
    [Parameter]
    [EditorRequired]
    public BasicList<TValue> Values { get; set; } = new();
    [Parameter]
    [EditorRequired]
    public Func<TValue, string>? RetrieveValue { get; set; }
    [Parameter]
    public EventCallback<BasicList<TValue>> ValuesChanged { get; set; }
    private enum EnumState
    {
        Old,
        Images,
        NotValid
    }
    private EnumState _state;
    protected override void OnParametersSet()
    {
        if (string.IsNullOrWhiteSpace(CheckBoxedImage) && string.IsNullOrWhiteSpace(UncheckedImage))
        {
            _state = EnumState.Old;
            return;
        }
        if (string.IsNullOrEmpty(CheckBoxedImage) && string.IsNullOrWhiteSpace(UncheckedImage) == false)
        {
            _state = EnumState.NotValid;
            return;
        }
        if (string.IsNullOrWhiteSpace(UncheckedImage) && string.IsNullOrWhiteSpace(CheckBoxedImage) == false)
        {
            _state = EnumState.NotValid;
            return;
        }
        _state = EnumState.Images;
    }
    private bool IsSelected(TValue info)
    {
        return Values.Any(xxx => xxx!.Equals(info));
    }
    public void OnCheckboxChanged(TValue item, object wasChecked)
    {
        bool rets = Convert.ToBoolean(wasChecked);
        if (rets)
        {
            AddSelectedItem(item);
            return;
        }
        RemoveSelectedItem(item);
    }
    private void AddSelectedItem(TValue item, bool doCheck = true)
    {
        if (doCheck)
        {
            if (IsSelected(item))
            {
                return;
            }
        }
        BasicList<TValue> temps = Values.ToBasicList();
        temps.Add(item);
        ValuesChanged.InvokeAsync(temps);
    }
    private void RemoveSelectedItem(TValue item, bool doCheck = true)
    {
        if (doCheck)
        {
            if (IsSelected(item) == false)
            {
                return;
            }
        }
        BasicList<TValue> temps = Values.ToBasicList();
        temps.RemoveSpecificItem(item);
        ValuesChanged.InvokeAsync(temps);
    }
    private void SelectUnselectItem(TValue item)
    {
        if (IsSelected(item))
        {
            RemoveSelectedItem(item, false);
            return;
        }
        AddSelectedItem(item, false);
    }
}