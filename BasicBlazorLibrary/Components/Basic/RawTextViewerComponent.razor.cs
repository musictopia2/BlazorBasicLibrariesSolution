namespace BasicBlazorLibrary.Components.Basic;
public partial class RawTextViewerComponent
{
    [Parameter]
    [EditorRequired]
    public string JsonText { get; set; } = "";
    [Parameter]
    public string Height { get; set; } = "100%";
    [Parameter]
    public string Width { get; set; } = "100%";
    [Parameter]
    public EventCallback OnClick { get; set; }
}