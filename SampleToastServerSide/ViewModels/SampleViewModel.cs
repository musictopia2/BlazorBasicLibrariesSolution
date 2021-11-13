using BasicBlazorLibrary.Components.Toasts;
using BasicBlazorLibrary.Layouts;

namespace SampleToastServerSide.ViewModels;
public class SampleViewModel : ISampleViewModel
{
    private readonly IToastComponent _toast;

    public SampleViewModel(IToastComponent toast)
    {
        _toast = toast;
    }
    public void RunTest()
    {
        _toast.ShowSuccessToast("This was successful");
        //UIPlatform.ShowSuccessToast("This was sucessful.  Try from multiple clients");
    }
}