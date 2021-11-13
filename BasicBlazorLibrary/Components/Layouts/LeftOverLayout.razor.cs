namespace BasicBlazorLibrary.Components.Layouts;
public partial class LeftOverLayout
{
    [Parameter]
    public RenderFragment? TopContent { get; set; }
    [Parameter]
    public RenderFragment? MainContent { get; set; }
    [Parameter]
    public RenderFragment? BottomContent { get; set; }
    public ElementReference? MainElement { get; private set; }
}