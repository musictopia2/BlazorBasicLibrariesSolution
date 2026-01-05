namespace BasicBlazorLibrary.Components.Modals;
public partial class PopupFullTapToClose
{

    [Parameter]
    public EventCallback CloseButtonClick { get; set; }



    [Parameter]
    public bool CanCloseAutomatically { get; set; } = true; //there are times when you cannot.

    [Parameter]
    public bool ShowHeaders { get; set; } = true;

    [Parameter]
    public string HeaderTitle { get; set; } = "";

    [Parameter]
    public string Width { get; set; } = "40vmin";
    [Parameter]
    public string Height { get; set; } = "";

    private string PanelSizeStyle()
    {
        var parts = new List<string>();

        if (string.IsNullOrWhiteSpace(Width) == false)
        {
            parts.Add($"width: {Width};");
        }

        if (string.IsNullOrWhiteSpace(Height) == false)
        {
            parts.Add($"height: {Height};");
        }

        return string.Join(" ", parts);
    }


    [Parameter]
    public RenderFragment? ChildContent { get; set; }
    [Parameter]
    public string Style { get; set; } = "";

    protected override void ClosePopup()
    {
        if (CanCloseAutomatically == false)
        {
            return; //becase you can't even close out for some reason.
        }
        base.ClosePopup();
    }

}