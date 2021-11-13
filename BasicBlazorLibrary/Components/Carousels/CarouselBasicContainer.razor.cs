namespace BasicBlazorLibrary.Components.Carousels;
public partial class CarouselBasicContainer
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; } //timed ones will not be the simple one.
    private IJSObjectReference? _reference;
    protected override async Task OnInitializedAsync()
    {
        _reference = await JS!.InvokeAsync<IJSObjectReference>("import", "./_content/BasicBlazorLibrary.Components/CarouselBasicContainer.razor.js");
    }
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await _reference!.InvokeVoidAsync("start");
        }
    }
}