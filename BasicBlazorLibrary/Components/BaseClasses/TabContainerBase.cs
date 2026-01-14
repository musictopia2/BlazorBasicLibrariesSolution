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

    // NEW: bindable selected tab key
    [Parameter] public string? ActiveKey { get; set; }
    [Parameter] public EventCallback<string?> ActiveKeyChanged { get; set; }

    // OPTIONAL: strongly-typed “tab changed”
    [Parameter] public EventCallback<T> TabChanged { get; set; }

    protected void AddPage(T tabPage)
    {
        Pages.Add(tabPage);

        // If caller supplied ActiveKey, prefer matching page
        if (!string.IsNullOrWhiteSpace(ActiveKey))
        {
            if (tabPage.TabKey == ActiveKey)
            {
                ActivatePageCore(tabPage, raiseEvents: false);
                StateHasChanged();
                return;
            }
        }

        // Existing behavior: first page becomes active
        if (Pages.Count == 1)
        {
            ActivatePageCore(tabPage, raiseEvents: false);
        }

        StateHasChanged();
    }
    TabPage? _page;
    public virtual void ActivatePage(T page)
    {
        // User action => raise callbacks
        ActivatePageCore(page, raiseEvents: true);
    }
    private void ActivatePageCore(T page, bool raiseEvents)
    {
        _page = page;
        ActivePage = page;

        if (!raiseEvents)
        {
            return;
        }

        var key = page.TabKey;

        // Keep ActiveKey in sync + notify
        if (ActiveKey != key)
        {
            ActiveKey = key;
            _ = ActiveKeyChanged.InvokeAsync(key);
        }

        _ = TabChanged.InvokeAsync(page);
    }

    void ITabContainer.AddPage(TabPage page) => AddPage((T)page);
    


    //void ITabContainer.AddPage(TabPage page)
    //{
    //    AddPage((T)page);
    //}
}