namespace BasicBlazorLibrary.Components.Basic;
public partial class MultiLineControl
{
    [Parameter]
    public string Value { get; set; } = "";
    [Parameter]
    public string Style { get; set; } = "";
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await JS!.ScrollToTopAsync();
        }
    }
}