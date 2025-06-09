using aa1 = BasicBlazorLibrary.Components.CssGrids.RowColumnHelpers;
namespace BasicBlazorLibrary.Components.Layouts;
public partial class TwoColumnLayout<T>
{
    [Parameter]
    [EditorRequired]
    public BasicList<T> RenderList { get; set; } = new(); //this is the only one that is absolutely required
    [Parameter]
    public RenderFragment<T>? ChildContent { get; set; }
    [Parameter]
    public string RowGap { get; set; } = "";
    [Parameter]
    public string ColumnGap { get; set; } = "";
    [Parameter]
    public string Style { get; set; } = "";
    [Parameter]
    public string Class { get; set; } = ""; //so you can add other parts to this.

    [Parameter]
    public bool UseCursor { get; set; } = true; //default to true.  but there can be cases where cursors would not be used.
    private string CursorCss => UseCursor ? "cursor: pointer;" : "";
    private static string Get2Columns()
    {
        return $"{aa1.OneSpread} {aa1.OneSpread}";
    }
}