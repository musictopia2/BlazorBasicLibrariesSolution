namespace BasicBlazorLibrary.Components.XML;
public partial class XMLViewerComponent
{
    [Parameter]
    [EditorRequired]
    public XElement? Element { get; set; }
    [Parameter]
    public string Height { get; set; } = "100%";
}