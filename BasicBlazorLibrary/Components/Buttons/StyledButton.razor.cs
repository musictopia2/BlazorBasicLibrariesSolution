namespace BasicBlazorLibrary.Components.Buttons;
public abstract partial class StyledButton
{
    protected abstract string ButtonClass { get; }
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
    [Parameter]
    public EventCallback BeforeConfirm { get; set; }
    [Parameter]
    public EventCallback RejectConfirm { get; set; } //this means for example, if you pause before confirming, then can unpause again.
    private bool _showConfirm;
    private void PrivateConfirm(bool confirm)
    {
        _showConfirm = false;
        if (confirm)
        {
            OnClick.InvokeAsync();
        }
        else if (RejectConfirm.HasDelegate == false)
        {
            return;
        }
        RejectConfirm.InvokeAsync();
    }
    private async Task PrivateClickAsync()
    {
        if (IsEnabled == false)
        {
            return;
        }
        if (ConfirmationMessage == "" && ConfirmationTitle == "")
        {
            await OnClick.InvokeAsync();
            return;
        }
        if (BeforeConfirm.HasDelegate)
        {
            await BeforeConfirm.InvokeAsync();
        }
        _showConfirm = true;
    }
    private string FinalStyle
    {
        get
        {
            var styles = new[]
            {
            string.IsNullOrWhiteSpace(Spacing) ? null : $"margin: {Spacing}",
            string.IsNullOrWhiteSpace(FontSize) ? null : $"font-size: {FontSize}",
            !IsEnabled ? $"color: {DisabledColor}" : null
        }
            .Where(s => !string.IsNullOrWhiteSpace(s))
            .ToList();

            if (styles.Count == 0)
                return "";

            return string.Join(";", styles) + ";";
        }
    }
}