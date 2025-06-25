namespace BasicBlazorLibrary.Components.CssManagement;
public partial class FullCssSetup
{
    [Parameter]
    [EditorRequired]
    public string AppStyleName { get; set; } = "";
    private string ScopedName => $"{AppStyleName}.styles.css";
}