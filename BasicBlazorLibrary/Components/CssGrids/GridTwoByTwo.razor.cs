using aa = BasicBlazorLibrary.Components.CssGrids.RowColumnHelpers;
namespace BasicBlazorLibrary.Components.CssGrids;
public partial class GridTwoByTwo
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
    /// <summary>
    /// If set to true then the layout of the grid will be "inline" instead of stretching to fill the container.
    /// </summary>
    /// <value>Default is false</value>
    [Parameter]
    public bool Inline { get; set; }
    [Parameter]
    public string RowGap { get; set; } = "";
    [Parameter]
    public string ColumnGap { get; set; } = "";
    [Parameter]
    public string Class { get; set; } = "";
    [Parameter]
    public string Style { get; set; } = "";

    [Parameter]
    public string ColumnLength { get; set; } = $"{aa.OneSpread}";
    //the length has to be the same for both.
    private string Get2SpreadContentEntries()
    {
        return $"{ColumnLength} {ColumnLength}";
    }
    private static string Get2AutoContentEntries()
    {
        return $"{aa.Auto} {aa.Auto}";
    }
}