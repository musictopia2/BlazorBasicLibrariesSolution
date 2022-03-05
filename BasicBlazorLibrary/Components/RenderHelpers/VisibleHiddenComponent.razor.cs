namespace BasicBlazorLibrary.Components.RenderHelpers;
public partial class VisibleHiddenComponent
{
    [Parameter]
    public bool Visible { get; set; } = true;
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
    [Parameter]
    public bool FullHeight { get; set; } = false; //i think i should have the choice whether to use full height for the visiblehiddencomponent.  because something may have needed it.
    private string GetDisplay => Visible ? "" : "none";
}