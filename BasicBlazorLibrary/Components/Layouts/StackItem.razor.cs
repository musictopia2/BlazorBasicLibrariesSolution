using System.Text;
namespace BasicBlazorLibrary.Components.Layouts;
public partial class StackItem
{
    [CascadingParameter]
    private StackLayout? Stack { get; set; }
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
    [Parameter]
    public bool Scrollable { get; set; }
    [Parameter]
    public string Style { get; set; } = "";

    [Parameter]
    public bool StopClickPropagation { get; set; } = false;

    /// <summary>
    /// Horizontal alignement (Example: "@Justify.Center")
    /// </summary>
    [Parameter]
    public string HorizontalAlignment { get; set; } = "";
    /// <summary>
    /// Vertical alignemtn (Example:"@Justify.Center")
    /// </summary>
    [Parameter]
    public string VerticalAlignment { get; set; } = "";
    [Parameter]
    public string Length { get; set; } = "max-content";
    [Parameter]
    public bool AlignEnd { get; set; }
    private int GetVisibleStyle
    {
        get
        {
            if (Visible)
            {
                return 1;
            }
            return 0;
        }
    }
    [Parameter]
    public bool Visible { get; set; } = true;
    [Parameter]
    public string BackgroundColor { get; set; } = "transparent";
    protected override void OnInitialized()
    {
        Stack!.AddChild(this);
        Stack.Refresh();
    }
    private string GetStyle()
    {
        StringBuilder sb = new();
        if (HorizontalAlignment != "")
        {
            sb.Append($"justify-content: {HorizontalAlignment};");
        }
        if (VerticalAlignment != "")
        {
            sb.Append($"align-content: {VerticalAlignment};");
        }
        if (Scrollable && Stack!.Orientation == EnumOrientation.Vertical)
        {
            sb.Append("overflow: auto; height: 100%;");
        }
        else if (Scrollable && Stack!.Orientation == EnumOrientation.Horizontal)
        {
            sb.Append("overflow: auto; width: 100%;");
        }
        sb.Append($"opacity: {GetVisibleStyle};");
        if (BackgroundColor != "transparent")
        {
            sb.Append($"background-color: {BackgroundColor};");
        }
        if (AlignEnd)
        {
            sb.Append("display: flex; justify-content: flex-end;");
        }
        if (Style != "")
        {
            sb.Append(Style);
        }
        string output = sb.ToString();
        return output;
    }
}