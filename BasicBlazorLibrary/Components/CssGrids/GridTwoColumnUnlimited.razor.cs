using aa1 = BasicBlazorLibrary.Components.CssGrids.RowColumnHelpers;
namespace BasicBlazorLibrary.Components.CssGrids;
public partial class GridTwoColumnUnlimited
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
    [Parameter]
    public bool Inline { get; set; }
    [Parameter]
    public string RowGap { get; set; } = "";
    [Parameter]
    public string ColumnGap { get; set; } = "";
    [Parameter]
    public bool MaxContent { get; set; } = true;
    [Parameter]
    public string Class { get; set; } = ""; //so you can add other parts to this.
    [Parameter]
    public string Style { get; set; } = "";
    private string Get2Columns()
    {
        if (MaxContent)
        {
            return $"{aa1.MaxContent} {aa1.OneSpread}";
        }
        return $"{aa1.MinContent} {aa1.OneSpread}";
    }
}