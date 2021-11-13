namespace BasicBlazorLibrary.Components.BaseClasses;
public abstract partial class ScrollableTableComponentBase<TValue>
{ 
    protected abstract RenderFragment Render(TValue item);
    [Parameter]
    public BasicList<string> Headers { get; set; } = new();
    [Parameter]
    public string Height { get; set; } = "100%";
    [Parameter]
    public string BackgroundColor { get; set; } = "White";
    [Parameter]
    public BasicList<TValue> ItemList { get; set; } = new();
    [Parameter]
    public RenderFragment<TValue>? ChildContent { get; set; }
    [CascadingParameter]
    private FullPageComponentBase? FullPageDetails { get; set; }
    private string GetColor()
    {
        if (BackgroundColor == "White" && FullPageDetails is not null)
        {
            return FullPageDetails.BackgroundColor;
        }
        return BackgroundColor;
    }
}