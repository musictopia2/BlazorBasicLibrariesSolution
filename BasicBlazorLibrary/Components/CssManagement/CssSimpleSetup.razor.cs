namespace BasicBlazorLibrary.Components.CssManagement;
public partial class CssSimpleSetup
{
    [Parameter]
    public string AppStyleName { get; set; } = "";

    [Parameter]
    public BasicList<string> CssFiles { get; set; } = [];
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
}