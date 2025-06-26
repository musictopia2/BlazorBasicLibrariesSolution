namespace BasicBlazorLibrary.Components.CssManagement;
public partial class FullCssSetup
{
    [Parameter]
    public string AppStyleName { get; set; } = "";
    private string ScopedName => $"{AppStyleName}.styles.css";
}