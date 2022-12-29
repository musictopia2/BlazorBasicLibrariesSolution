namespace BasicBlazorLibrary.Components.Basic;
public partial class ScreenReaderOnlyComponent
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
}