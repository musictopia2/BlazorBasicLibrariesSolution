using BasicBlazorLibrary.Components.Tabs;
namespace BasicBlazorLibrary.Components.BaseClasses;
public abstract class TabContainerBase<T> : ComponentBase, ITabContainer
    where T : TabPage
{
    public T? ActivePage { get; set; }
    public BasicList<T> Pages { get; set; } = new();
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
    TabPage? ITabContainer.ActivePage => _page;
    [Parameter]
    public bool CollapsePages { get; set; }
    protected void AddPage(T tabPage)
    {
        Pages.Add(tabPage);
        if (Pages.Count == 1)
        {
            ActivatePage(tabPage);
        }
        StateHasChanged();
    }
    TabPage? _page;
    public virtual void ActivatePage(T page)
    {
        _page = page;
        ActivePage = page;
    }
    void ITabContainer.AddPage(TabPage page)
    {
        AddPage((T)page);
    }
}