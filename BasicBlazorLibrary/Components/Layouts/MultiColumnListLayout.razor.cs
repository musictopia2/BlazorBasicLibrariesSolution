using aa1 = BasicBlazorLibrary.Components.CssGrids.RowColumnHelpers;
namespace BasicBlazorLibrary.Components.Layouts;
public partial class MultiColumnListLayout<T>
{
    [Parameter]
    public BasicList<T> RenderList { get; set; } = []; //this is the only one that is absolutely required
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

    [Parameter]
    public int Columns { get; set; } = 3; //if doing two, just use the two one then.

    private string CursorCss => UseCursor ? "cursor: pointer;" : "";
    private string GetColumns()
    {
        if (Columns <= 1)
        {
            return aa1.OneSpread; // Fallback to single column
        }
        return string.Join(" ", Enumerable.Repeat(aa1.OneSpread, Columns));
    }
}