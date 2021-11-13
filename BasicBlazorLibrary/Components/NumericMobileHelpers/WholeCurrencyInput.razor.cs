using BasicBlazorLibrary.Components.Layouts;
namespace BasicBlazorLibrary.Components.NumericMobileHelpers;
public partial class WholeCurrencyInput
{
    //this is intended for mobile.  this means no keyboard stuff.  also means no tab orders either.
    [Parameter]
    public decimal MaximumPrice { get; set; }
    [Parameter]
    public decimal Value { get; set; }
    [Parameter]
    public EventCallback<decimal> ValueChanged { get; set; }
    [Parameter]
    public string Title { get; set; } = "Price"; //default will be price.
    [Parameter]
    public string FontSize { get; set; } = "3rem"; //default at 3rem.  but you can make it as flexible as you want for this.
    [Parameter]
    public string TextColor { get; set; } = "black"; //i can manually type the data (hopefully works).
    [Parameter]
    public string BackgroundColor { get; set; } = "white"; //allow more flexibility if needed.
    private string GetText => Value > 0 ? Value.ToCurrency(0, false) : "0";
    private string MaximumText => $"Max {MaximumPrice.ToCurrency(0, false)}";
    [Parameter]
    public EnumOrientation Orientation { get; set; } = EnumOrientation.Vertical; //default to vertical but give many choices for this.
    private bool _visible;
    private void ShowPopup()
    {
        _visible = true;
    }
    private void OnChangeNumeric(decimal value)
    {
        ValueChanged.InvokeAsync(value);
    }
}