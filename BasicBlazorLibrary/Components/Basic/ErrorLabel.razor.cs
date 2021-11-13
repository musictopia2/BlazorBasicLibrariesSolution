namespace BasicBlazorLibrary.Components.Basic;
public partial class ErrorLabel
{
    [Parameter]
    [EditorRequired]
    public string ErrorMessage { get; set; } = "";
}
