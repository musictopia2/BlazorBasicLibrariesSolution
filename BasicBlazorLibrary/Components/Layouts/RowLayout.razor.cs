namespace BasicBlazorLibrary.Components.Layouts;
public partial class RowLayout
{
    //this specialize in rows alone.  no specialized stuff with it so content can be scrolled.
    [Parameter]
    public string Rows { get; set; } = "";
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
    [Parameter]
    public string RowGap { get; set; } = "2px";
    [Parameter]
    public string Width { get; set; } = "100%";
    [Parameter]
    public string Height { get; set; } = "100%";
}