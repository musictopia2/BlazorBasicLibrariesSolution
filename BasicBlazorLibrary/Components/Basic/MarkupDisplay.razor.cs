namespace BasicBlazorLibrary.Components.Basic;
public partial class MarkupDisplay
{
    [Parameter]
    [EditorRequired]
    public string Text { get; set; } = "";
}