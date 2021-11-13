namespace BasicBlazorLibrary.Components.Basic;
public partial class TwoColumnRowDisplay
{
    [Parameter]
    [EditorRequired]
    public string FirstWidth { get; set; } = ""; //make it flexible enough to handle anything.
    [Parameter]
    [EditorRequired]
    public string SecondWidth { get; set; } = "";
    [Parameter]
    public RenderFragment? FirstContent { get; set; }
    [Parameter]
    public RenderFragment? SecondContent { get; set; }
    [Parameter]
    public string CellBorderColor { get; set; } = "black";
    [Parameter]
    public string CellStyle { get; set; } = "";
}