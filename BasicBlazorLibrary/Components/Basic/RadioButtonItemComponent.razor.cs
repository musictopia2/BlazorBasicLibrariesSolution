using System.Globalization;
namespace BasicBlazorLibrary.Components.Basic;
public partial class RadioButtonItemComponent<TValue>
{
    [CascadingParameter]
    private RadioButtonGroupComponent? Group { get; set; }
    private string GetName => Group!.Name;
    [Parameter]
    public TValue? SelectedValue { get; set; }
    [Parameter]
    public float Zoom { get; set; } = 1.5f;
    [Parameter]
    public string WidthHeight { get; set; } = "";
    [Parameter]
    public TValue? Value { get; set; }
    [Parameter]
    public bool ShowLabel { get; set; }
    private string GetText => SelectedValue!.ToString()!.TextWithSpaces();
    [Parameter]
    public EventCallback<TValue> ValueChanged { get; set; }
    private bool Checked => SelectedValue!.Equals(Value);
    private void OnChange(ChangeEventArgs args)
    {
        var value = args.Value;
        bool rets = BindConverter.TryConvertTo<TValue>(value, CultureInfo.CurrentCulture, out var outs);
        if (rets == false)
        {
            throw new CustomBasicException("Unable to convert to expected variable type.  Rethink");
        }
        ValueChanged.InvokeAsync(outs);
    }
    private void ManuelChange()
    {
        ValueChanged.InvokeAsync(SelectedValue);
    }
}