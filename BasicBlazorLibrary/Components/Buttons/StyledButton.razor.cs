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
    public string Spacing { get; set; } = ""; //i like the idea of defaulting to blank now since i have global.
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
            var styles = new List<string>();
            if (!string.IsNullOrWhiteSpace(Spacing))
            {
                styles.Add($"margin: {Spacing}");
            }
            else if (string.IsNullOrWhiteSpace(StyleDefaults.DefaultSpacing) == false)
            {
                styles.Add($"margin: {StyleDefaults.DefaultSpacing}");
            }
            if (!string.IsNullOrWhiteSpace(FontSize))
            {
                styles.Add($"font-size: {FontSize}");
            }
            else if (string.IsNullOrWhiteSpace(StyleDefaults.DefaultFontSize) == false)
            {
                styles.Add($"font-size: {StyleDefaults.DefaultFontSize}");
            }
            if (!IsEnabled)
            {
                styles.Add($"color: {DisabledColor}");
            }

            return styles.Count > 0 ? string.Join(";", styles) + ";" : "";

        }
    }
}