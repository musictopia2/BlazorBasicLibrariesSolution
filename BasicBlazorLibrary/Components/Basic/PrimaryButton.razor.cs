namespace BasicBlazorLibrary.Components.Basic;
public partial class PrimaryButton
{
    [Parameter]
    public EventCallback OnClick { get; set; }
    [Parameter]
    public string Style { get; set; } = "";
    [Parameter]
    public bool IsEnabled { get; set; } = true;
    [Parameter]
    public string Spacing { get; set; } = "5px";
    [Parameter]
    public string FontSize { get; set; } = "";
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
    private static string DisabledColor => cc1.LightGray.ToWebColor();
    [Parameter]
    public string ConfirmationMessage { get; set; } = "";
    [Parameter]
    public string ConfirmationTitle { get; set; } = "";
    private bool _showConfirm;
    private void PrivateConfirm(bool confirm)
    {
        _showConfirm = false;
        if (confirm)
        {
            OnClick.InvokeAsync();
        }
    }
    private void PrivateClick()
    {
        if (ConfirmationMessage == "" && ConfirmationTitle == "")
        {
            OnClick.InvokeAsync();
            return;
        }
        _showConfirm = true;
    }
}