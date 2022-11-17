namespace BasicBlazorLibrary.Components.Inputs;
public partial class InputEnterText
{
    [Parameter]
    public bool FullWidth { get; set; }
    private string GetStyle() => FullWidth || Item is not null ? "width: 99%;" : "";
    [Parameter]
    public string Title { get; set; } = "";
    [Parameter]
    public string HeaderFontSize { get; set; } = "";
    private string GetHeaderFontSize => HeaderFontSize == "" ? "1em" : HeaderFontSize;
    private string IsSpells => SpellCheck ? "true" : "false";
}
