namespace BasicBlazorLibrary.Components.Basic;
public partial class FrameSimpleComponent
{
    [Parameter]
    public string BorderColor { get; set; } = "black";
    [Parameter]
    public bool Bold { get; set; }
    [Parameter]
    public string Text { get; set; } = "";
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
}