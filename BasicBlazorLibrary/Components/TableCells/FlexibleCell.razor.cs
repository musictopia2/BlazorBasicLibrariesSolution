namespace BasicBlazorLibrary.Components.TableCells;
public partial class FlexibleCell
{
    [Parameter]
    public string ColumnWidth { get; set; } = "100px"; //default to 100.  but can be anything you want.
    [Parameter]
    public bool HasRowGap { get; set; } = false;
    private string GetPadding => HasRowGap ? "20px" : "";
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
}