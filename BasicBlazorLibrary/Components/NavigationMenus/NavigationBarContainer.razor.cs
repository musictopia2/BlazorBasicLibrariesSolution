using System.Drawing;
namespace BasicBlazorLibrary.Components.NavigationMenus;
public partial class NavigationBarContainer
{
    [Parameter]
    public string Title { get; set; } = "";
    private bool _showBar = false;
    [Parameter]
    public RenderFragment? MainContent { get; set; }
    [Parameter]
    public RenderFragment? BarContent { get; set; }
    [Parameter]
    public EventCallback BackClicked { get; set; }
    [Parameter]
    public bool ShowBack { get; set; } = true;
    [Parameter]
    public EventCallback CloseClicked { get; set; }
    [Parameter]
    public string ArrowHeight { get; set; } = "70px";
    [Parameter]
    public string CloseHeight { get; set; } = "2rem";
    [Parameter]
    public string MainBackgroundColor { get; set; } = cc1.Blue.ToWebColor();
    [Parameter]
    public string MenuBackgroundColor { get; set; } = cc1.Black.ToWebColor();
    [Parameter]
    public string MainTextColor { get; set; } = cc1.White.ToWebColor();
    [Parameter]
    public string MenuTextColor { get; set; } = cc1.White.ToWebColor();
    [Parameter]
    public string MenuFontSize { get; set; } = "1.5rem";
    [Parameter]
    public string Padding { get; set; } = "10px";
    [Parameter]
    public string MainFontSize { get; set; } = "1.5rem";
    [Parameter]
    public string CircleSize { get; set; } = "10px";
    [Parameter]
    public string CircleColor { get; set; } = cc1.White.ToWebColor();
    [Parameter]
    public string MenuHeight { get; set; } = "300px";
    [Parameter]
    public string MenuWidth { get; set; } = "50vmin";
    /// <summary>
    /// this is the height when the menu shows up.  helps so can make it easier to click proper item on phones.
    /// </summary>
    [Parameter]
    public string ItemHeight { get; set; } = "";
    [Parameter]
    public BasicList<MenuItem> MenuList { get; set; } = new();
    [Parameter]
    public bool AlwaysShowBar { get; set; } = false;
    [Parameter]
    public bool CanChangeWithoutTabs { get; set; } = false;
    private SizeF _viewPort = new(40, 40);
    private string GetContainer
    {
        get
        {
            string output = CircleSize.ContainerHeight(4, _viewPort);
            return output;
        }
    }
    protected override void OnInitialized()
    {
        _showMenu = false;
        _showBar = AlwaysShowBar;
    }
    protected override void OnParametersSet()
    {
        if (CanChangeWithoutTabs)
        {
            _showBar = AlwaysShowBar;
        }
        base.OnParametersSet();
    }
    private bool _showMenu;
    private string GetFirstClass()
    {
        if (_showBar == false)
        {
            return "hidden";
        }
        return "";
    }
    public void ChangeBar(bool display)
    {
        if (AlwaysShowBar)
        {
            return;
        }
        _showBar = display;
        StateHasChanged();
    }
}