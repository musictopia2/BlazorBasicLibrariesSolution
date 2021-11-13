using System.Drawing;
namespace BasicBlazorLibrary.Components.Layouts;
public partial class CanvasLayout
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
    [Parameter]
    public string ContainerHeight { get; set; } = "";
    [Parameter]
    public string ContainerWidth { get; set; } = "";
    [Parameter]
    public SizeF ViewPort { get; set; }
    [Parameter]
    public string BackgroundColor { get; set; } = cc.Transparent.ToWebColor();
    [Parameter]
    public EventCallback Clicked { get; set; }
    private async Task Submit()
    {
        if (Clicked.HasDelegate)
        {
            await Clicked.InvokeAsync();
        }
    }
}