namespace BasicBlazorLibrary.Components.CssManagement;
public abstract partial class CssSectionComponentBase
{
    protected abstract string SectionName { get; }
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
}