namespace BasicBlazorLibrary.Components.Basic;
public partial class RawTextViewerComponent
{
    [Parameter]
    [EditorRequired]
    public string Text { get; set; } = "";
    [Parameter]
    public string Height { get; set; } = "100%";
    [Parameter]
    public string Width { get; set; } = "100%";
    [Parameter]
    public EventCallback OnClick { get; set; }
    [Parameter]
    public bool DisableOverflow { get; set; }
    private string GetClass => DisableOverflow ? "" : "auto";

}