using CommonBasicLibraries.BasicUIProcesses;
namespace BasicBlazorLibrary.Components.Toasts;
public class ToastService : IToastComponent
{
    public BlazoredToasts? Toast { get; set; }
    private void CheckToast()
    {
        if (Toast is null)
        {
            throw new CustomBasicException("Must have the blazor toast to show that toast");
        }
    }
    void IToast.ShowInfoToast(string message)
    {
        CheckToast();
        Toast!.ShowToast(EnumToastLevel.Info, message);
    }

    void IToast.ShowSuccessToast(string message)
    {
        CheckToast();
        Toast!.ShowToast(EnumToastLevel.Success, message);
    }

    void IToast.ShowUserErrorToast(string message)
    {
        CheckToast();
        Toast!.ShowToast(EnumToastLevel.Error, message);
    }

    void IToast.ShowWarningToast(string message)
    {
        CheckToast();
        Toast!.ShowToast(EnumToastLevel.Warning, message);
    }
}