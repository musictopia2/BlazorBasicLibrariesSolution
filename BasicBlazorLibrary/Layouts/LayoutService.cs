using CommonBasicLibraries.BasicUIProcesses;
namespace BasicBlazorLibrary.Layouts;
public class LayoutService : ILayout
{
    public StartLayout? Layout { get; set; }
    private void Check()
    {
        if (Layout is null)
        {
            throw new CustomBasicException("Must have a layout");
        }
    }
    void IExit.ExitApp()
    {
        Check();
        Layout!.ExitApp();
    }

    async Task IMessageBox.ShowMessageAsync(string message)
    {
        Check();
        await Layout!.ShowMessageAsync(message);
    }

    void ISystemError.ShowSystemError(string message)
    {
        Check();
        Layout!.ShowSystemError(message);
    }
}
