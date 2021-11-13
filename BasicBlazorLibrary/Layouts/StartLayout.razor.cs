using aa = CommonBasicLibraries.BasicUIProcesses.UIPlatform;
namespace BasicBlazorLibrary.Layouts;
public partial class StartLayout
{
    [Inject]
    private ILayout? Layout { get; set; }
    public static string DefaultGridHeight => "730px";
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
    private string ErrorMessage { get; set; } = "";
    protected bool Exited { get; set; }
    [Parameter]
    public bool ShowFriendlyError { get; set; } = false;
    private bool _messageOpened;
    private string _messageShown = "";
    private Task ClosePopupAsync()
    {
        _messageShown = "";
        _messageOpened = false;
        return Task.CompletedTask;
    }
    internal async Task ShowMessageAsync(string message)
    {
        await InvokeAsync(async () =>
        {
            _messageShown = message;
            _messageOpened = true;
            StateHasChanged();
            do
            {
                await Task.Delay(50);
                if (_messageOpened == false)
                {
                    return;
                }
            } while (true);
        });
    }
    internal void ExitApp()
    {
        Exited = true;
        aa.ShowSuccessToast("Should close out because you are finished with everything.  Please close out manually");
        _ = JS!.ExitFullScreen(); //has to exit full screen if you are closing out.
        StateHasChanged();
    }
    internal void ShowSystemError(string message)
    {
        if (ShowFriendlyError)
        {
            aa.ShowUserErrorToast($"System Error: {message}");
        }
        else
        {
            throw new CustomBasicException(message);
        }
    }
    protected override void OnInitialized()
    {
        if (bb.OS == bb.EnumOS.WindowsDT)
        {
            return;
        }
        aa.ExitApp = Layout!.ExitApp;
        aa.ShowMessageAsync = Layout.ShowMessageAsync;
        aa.ShowSystemError = Layout.ShowSystemError;

        //aa.ExitApp = () =>
        //{
           
        //};
        //aa.ShowMessageAsync = ShowMessageAsync;
        //aa.ShowSystemError = message =>
        //{
            
        //};
    }
}