namespace BasicBlazorLibrary.Components.Basic;
public class Redirect : ComponentBase
{
    [Inject]
    private NavigationManager? Navigates { get; set; }
    [Parameter]
    [EditorRequired]
    public string Url { get; set; } = "";
    protected override void OnInitialized()
    {
        Navigates!.NavigateTo(Url);
    }
}