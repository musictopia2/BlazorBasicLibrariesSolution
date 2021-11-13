namespace BasicBlazorLibrary.Components.Basic;
public partial class CustomLabel
{
    [Parameter]
    public bool Bold { get; set; }
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
    [Parameter]
    public string FontSize { get; set; } = "15px";
    [Parameter]
    public bool StartsOnNextLine { get; set; }
    [Parameter]
    public string Style { get; set; } = "";
    ////can use off the shelf part for class.
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object> InputAttributes { get; set; } =
    new()
    {
        { "class", "" }
    };
    private string GetStyle
    {
        get
        {
            string starts = "font-weight: bold;";
            if (Bold == false)
            {
                starts = "";
            }

            if (Style != "")
            {
                string otherText = Style;
                if (otherText.EndsWith(";") == false)
                {
                    otherText = $"{otherText};";
                }
                return $"{starts} {otherText}";
            }
            return starts;
        }
    }
}