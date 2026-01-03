using aa2 = BasicBlazorLibrary.Components.CssGrids.RowColumnHelpers;
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
    [Parameter]
    public bool StopClickPropagation { get; set; } = false;
    private string Get2Columns()
    {
        if (MaxContent)
        {
            return $"{aa2.MaxContent} {aa2.OneSpread}";
        }
        return $"{aa2.MinContent} {aa2.OneSpread}";
    }
}