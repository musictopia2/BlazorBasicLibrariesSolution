namespace BasicBlazorLibrary.Components.Layouts;
public partial class WrapLayout<T>
{
    [Parameter]
    [EditorRequired]
    public IEnumerable<T> RenderList { get; set; } = []; //this is the only one that is absolutely required
    [Parameter]
    public RenderFragment<T>? ChildContent { get; set; }
    [Parameter]
    public string ColumnWidth { get; set; } = "100px"; //can be whatever you want.
    [Parameter]
    public string Margins { get; set; } = "2px";
    [Parameter]
    public string Style { get; set; } = "";
    [Parameter]
    public bool UseCursor { get; set; } = true; //default to true.  but there can be cases where cursors would not be used.
    private string CursorCss => UseCursor ? "cursor: pointer;" : "";
}