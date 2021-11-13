namespace BasicBlazorLibrary.Components.TableCells;
public partial class LabelCell
{
    [Parameter]
    public string RowGap { get; set; } = "10px";
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
}