namespace BasicBlazorLibrary.Components.Carousels;
public partial class CarouselBasicList<TValue>
{
    [Parameter]
    public BasicList<TValue> RenderList { get; set; } = new();
    [Parameter]
    public RenderFragment<TValue>? ChildContent { get; set; }
}