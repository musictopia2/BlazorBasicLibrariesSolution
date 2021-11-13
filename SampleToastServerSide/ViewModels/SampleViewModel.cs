using BasicBlazorLibrary.Components.Toasts;
namespace SampleToastServerSide.ViewModels;
public class SampleViewModel : ISampleViewModel
{
    private readonly IToastComponent _toast;
    private readonly IMessageBox _message;

    public SampleViewModel(IToastComponent toast, IMessageBox message)
    {
        _toast = toast;
        _message = message;
    }
    async Task ISampleViewModel.TestMessageBoxAsync()
    {
        await _message.ShowMessageAsync("Message Successful 1");
        await _message.ShowMessageAsync("Message Successful 2");
    }
    void ISampleViewModel.TestToast()
    {
        _toast.ShowSuccessToast("This was successful");
    }
}