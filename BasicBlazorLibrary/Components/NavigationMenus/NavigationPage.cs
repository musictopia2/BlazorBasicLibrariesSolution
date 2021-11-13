using BasicBlazorLibrary.Components.Tabs;
namespace BasicBlazorLibrary.Components.NavigationMenus;
public class NavigationPage : TabPage
{
    [Parameter]
    public bool ShowNavigationBar { get; set; }
}