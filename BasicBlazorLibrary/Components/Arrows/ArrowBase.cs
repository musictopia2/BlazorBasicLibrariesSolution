namespace BasicBlazorLibrary.Components.Arrows;
public class ArrowBase : ComponentBase
{
    [Parameter]
    public EventCallback Clicked { get; set; }
    [Parameter]
    public string BackgroundColor { get; set; } = cc1.Black.ToWebColor;
    [Parameter]
    public string TargetHeight { get; set; } = "";
    [Parameter]
    public string StrokeWidth { get; set; } = "1px";
    [Parameter]
    public string StrokeColor { get; set; } = cc1.Transparent.ToWebColor;
    [Parameter]
    public string TargetWidth { get; set; } = "";
    protected string GetSvgStyle()
    {
        if (TargetHeight == "" && TargetWidth == "")
        {
            return "";
        }
        if (TargetHeight != "" && TargetWidth != "")
        {
            return "";
        }

        if (TargetHeight != "")
        {
            return $"height: {TargetHeight}";
        }
        return $"width: {TargetWidth}";
    }
}