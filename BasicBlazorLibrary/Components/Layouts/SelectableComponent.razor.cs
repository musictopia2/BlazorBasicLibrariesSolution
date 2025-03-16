namespace BasicBlazorLibrary.Components.Layouts;
public partial class SelectableComponent<T> where T : ISelectable
{
    [Parameter]
    public string ColumnWidth { get; set; } = "300px";
    [Parameter]
    public EventCallback OnSelected { get; set; }
    [Parameter]
    public RenderFragment<T>? ChildContent { get; set; }
    [Parameter]
    [EditorRequired]
    public BasicList<T>? ItemList { get; set; }
    [Parameter]
    public bool UseCursor { get; set; }
    private static string CssName(T item) => item.IsSelected ? "selected" : "regular";
    public void ItemClicked(T item)
    {
        item.IsSelected = !item.IsSelected;
        OnSelected.InvokeAsync(); //so the parent can do something else too (since the parent needs to possiblyl update the count or do other things).
    }
}