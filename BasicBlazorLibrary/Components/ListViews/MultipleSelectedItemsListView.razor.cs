namespace BasicBlazorLibrary.Components.ListViews;
public partial class MultipleSelectedItemsListView<TValue>
{
    [Parameter]
    public BasicList<TValue> SelectedList { get; set; } = new();
    [Parameter]
    public string HighlightColor { get; set; } = cc1.Lime.ToWebColor;
    [Parameter]
    public string HoverColor { get; set; } = cc1.LightYellow.ToWebColor;
    [Parameter]
    public EventCallback OnChangeSelectedItems { get; set; }
    private string GetHoverColor(TValue selectedItem)
    {
        if (selectedItem is null)
        {
            return "";
        }
        if (SelectedList.Contains(selectedItem) == false)
        {
            return HoverColor;
        }
        return HighlightColor;
    }
    private string GetBackgroundColor(TValue selectedItem)
    {
        if (selectedItem is null)
        {
            return "";
        }
        if (SelectedList.Contains(selectedItem) == false)
        {
            return "";
        }
        return HighlightColor;
    }
    private string GetColorStyle(TValue selectedItem)
    {
        if (selectedItem is null)
        {
            return "";
        }
        if (SelectedList.Contains(selectedItem) == false)
        {
            return "";
        }
        return $"background-color: {HighlightColor};";
    }
    private void OnItemClick(TValue selectedItem)
    {
        if (selectedItem is null)
        {
            return;
        }
        if (SelectedList.Contains(selectedItem))
        {
            SelectedList.RemoveSpecificItem(selectedItem);
            OnChangeSelectedItems.InvokeAsync();
            return;
        }
        SelectedList.Add(selectedItem);
        OnChangeSelectedItems.InvokeAsync();
    }
}