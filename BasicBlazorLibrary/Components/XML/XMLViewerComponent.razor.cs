namespace BasicBlazorLibrary.Components.XML;
public partial class XMLViewerComponent
{
    [Parameter]
    [EditorRequired]
    public XElement? Element { get; set; }
    [Parameter]
    public string Height { get; set; } = "100%";
    [Parameter]
    public string Width { get; set; } = "100%";
    [Parameter]
    public EventCallback<XElement> OnClick { get; set; } //looks like needs the entire element.  so if there are several elements on the page, then can see what needs to be done.
    private async Task PrivateClickAsync()
    {
        if (OnClick.HasDelegate == false)
        {
            return;
        }
        await OnClick.InvokeAsync(Element);
    }
    private static string GetBackgroundColor => cc1.White.ToWebColor();
    private string GetHoverColor => OnClick.HasDelegate ? cc1.LightYellow.ToWebColor() : GetBackgroundColor;
}