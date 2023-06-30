namespace BasicBlazorLibrary.Components.XML.InternalHelpers;
public partial class HtmlTagComponent
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
    [Parameter]
    public int HowManyTabs { get; set; }
}