namespace BasicBlazorLibrary.Components.ListViews;
public partial class SingleSelectedItemListView<TValue>
{
    [Parameter]
    public TValue? SelectedItem { get; set; }
    [Parameter]
    public EventCallback<TValue?> SelectedItemChanged { get; set; }
    [Parameter]
    public string HighlightColor { get; set; } = cc.Lime.ToWebColor();
    [Parameter]
    public string HoverColor { get; set; } = cc.LightYellow.ToWebColor();
    private string GetHoverColor(TValue selectedItem)
    {
        if (selectedItem is null)
        {
            return "";
        }
        if (selectedItem.Equals(SelectedItem) == false)
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
        if (selectedItem.Equals(SelectedItem) == false)
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
        if (selectedItem.Equals(SelectedItem) == false)
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
        if (selectedItem.Equals(SelectedItem))
        {
            SelectedItemChanged.InvokeAsync(default);
            return;
        }
        SelectedItemChanged.InvokeAsync(selectedItem);
    }
}