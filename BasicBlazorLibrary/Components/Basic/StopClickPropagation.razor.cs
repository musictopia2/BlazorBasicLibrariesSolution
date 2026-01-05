namespace BasicBlazorLibrary.Components.Basic;
public partial class StopClickPropagation
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    [Parameter]
    public string Class { get; set; } = "";
    [Parameter]
    public string Style { get; set; } = "";
}