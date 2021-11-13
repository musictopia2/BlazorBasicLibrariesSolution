namespace BasicBlazorLibrary.Components.Basic;
public partial class LabelPairComponent
{
    [Parameter]
    [EditorRequired]
    public string HeaderText { get; set; } = "";
    [Parameter]
    [EditorRequired]
    public object MainText { get; set; } = "";
    [Parameter]
    public bool UseGrid { get; set; }
    [Parameter]
    public int Row { get; set; } = 0;
    [Parameter]
    public string FontSize { get; set; } = "1em";
    [Parameter]
    public bool BoldHeader { get; set; }
    private string GetStyle => BoldHeader ? "font-weight: bold;" : "";
}