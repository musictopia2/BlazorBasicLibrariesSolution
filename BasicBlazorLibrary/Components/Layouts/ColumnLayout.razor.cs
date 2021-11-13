namespace BasicBlazorLibrary.Components.Layouts;
public partial class ColumnLayout
{
    [Parameter]
    public string Columns { get; set; } = "";
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
    [Parameter]
    public string ColumnGap { get; set; } = "2px";
    [Parameter]
    public string Width { get; set; } = "100%";
    [Parameter]
    public string Height { get; set; } = "100%";
}