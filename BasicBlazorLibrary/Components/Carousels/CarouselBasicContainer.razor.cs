namespace BasicBlazorLibrary.Components.Carousels;
public partial class CarouselBasicContainer
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; } //timed ones will not be the simple one.
    private SimpleCarouselClass? _helps;
    protected override void OnInitialized()
    {
        _helps = new(JS!);
        base.OnInitialized();
    }
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await _helps!.StartAsync();
        }
    }
}