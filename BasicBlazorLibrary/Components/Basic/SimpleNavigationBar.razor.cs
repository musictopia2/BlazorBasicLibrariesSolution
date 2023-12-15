namespace BasicBlazorLibrary.Components.Basic;
public partial class SimpleNavigationBar
{
    [Parameter]
    public bool HasBack { get; set; } = true;//on main page, will not have backs.  but single pages would have back.
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
    [Parameter]
    public string Title { get; set; } = ""; //i think this is fine.
    [Parameter]
    public EventCallback BackClicked { get; set; }
    [Parameter]
    public string ArrowColor { get; set; } = "aqua";
    [Parameter]
    public string ArrowStroke { get; set; } = "black";
}