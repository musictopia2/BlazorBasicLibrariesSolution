using BasicBlazorLibrary.Components.Tabs;
namespace BasicBlazorLibrary.Components.NavigationMenus;
public partial class NavigationTabContainer : IBarContainer<NavigationPage>
{
    [CascadingParameter]
    private NavigationBarContainer? Bar { get; set; }
    [Parameter]
    public string ActiveColor { get; set; } = cc1.White.ToWebColor;
    [Parameter]
    public string NormalColor { get; set; } = cc1.LightGray.ToWebColor; //i think.
    [Parameter]
    public string FontSize { get; set; } = "1.2rem"; //can be whatever i need it to be.
    [Parameter]
    public string Padding { get; set; } = "10px;";
    [Parameter]
    public string BackgroundColor { get; set; } = cc1.Black.ToWebColor;
    public override void ActivatePage(NavigationPage page)
    {
        base.ActivatePage(page);
        Bar!.ChangeBar(page.ShowNavigationBar);
    }
}