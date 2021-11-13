namespace BasicBlazorLibrary.Components.Basic;
public partial class RadioButtonGroupComponent
{
    [Parameter]
    [EditorRequired]
    public string Name { get; set; } = "";
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
}
