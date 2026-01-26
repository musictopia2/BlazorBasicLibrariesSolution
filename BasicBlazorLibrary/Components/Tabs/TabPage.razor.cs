namespace BasicBlazorLibrary.Components.Tabs;
public partial class TabPage
{
    [CascadingParameter]
    private ITabContainer? TabContainer { get; set; }
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Optional: lets the tab "header" be an image, icon+text, padlock overlay, etc.
    /// If null, the tab falls back to Text.
    /// </summary>
    [Parameter] public RenderFragment? HeaderTemplate { get; set; }

    private string GetDisplay => TabContainer!.ActivePage == this ? "" : "none";
    [Parameter]
    public string Text { get; set; } = "";

    [Parameter] public string? Key { get; set; }   // NEW

    internal string TabKey => string.IsNullOrWhiteSpace(Key) ? Text : Key;

    protected override void OnInitialized()
    {
        if (TabContainer == null)
        {
            throw new CustomBasicException("Needs tab container");
        }
        base.OnInitialized();
        TabContainer.AddPage(this);
    }
}