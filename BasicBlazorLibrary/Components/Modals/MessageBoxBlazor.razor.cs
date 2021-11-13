namespace BasicBlazorLibrary.Components.Modals;
public partial class MessageBoxBlazor
{
    [Parameter]
    public string Message { get; set; } = "";
    [Parameter]
    public EventCallback CloseClicked { get; set; }
    private BasicList<string> Messages()
    {
        return Message.Split(Constants.VBLF).ToBasicList();
    }
}