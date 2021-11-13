using Microsoft.AspNetCore.Components.Routing;
namespace BasicBlazorLibrary.Components.Toasts;
public partial class BlazoredToasts
{
    [Inject]
    private IToastComponent? Toast { get; set; }
    [Inject] private NavigationManager? NavigationManager { get; set; }
    [Parameter] public string InfoClass { get; set; } = "";
    [Parameter] public string SuccessClass { get; set; } = "";
    [Parameter] public string WarningClass { get; set; } = "";
    [Parameter] public string ErrorClass { get; set; } = "";
    [Parameter] public int Timeout { get; set; } = 5;
    [Parameter] public bool RemoveToastsOnNavigation { get; set; }
    [Parameter] public bool ShowProgressBar { get; set; }
    private ToastSettings BuildToastSettings(EnumToastLevel level, RenderFragment message, string heading)
    {
        return level switch
        {
            EnumToastLevel.Error => new ToastSettings(string.IsNullOrWhiteSpace(heading) ? "Error" : heading, message, "blazored-toast-error", ErrorClass, ShowProgressBar),
            EnumToastLevel.Info => new ToastSettings(string.IsNullOrWhiteSpace(heading) ? "Info" : heading, message, "blazored-toast-info", InfoClass, ShowProgressBar),
            EnumToastLevel.Success => new ToastSettings(string.IsNullOrWhiteSpace(heading) ? "Success" : heading, message, "blazored-toast-success", SuccessClass, ShowProgressBar),
            EnumToastLevel.Warning => new ToastSettings(string.IsNullOrWhiteSpace(heading) ? "Warning" : heading, message, "blazored-toast-warning", WarningClass, ShowProgressBar),
            _ => throw new InvalidOperationException(),
        };
    }
    protected override void OnInitialized()
    {
        Toast!.Toast = this;
        //aa.ShowUserErrorToast = Toast.ShowUserErrorToast;
        //aa.ShowInfoToast = Toast.ShowInfoToast;
        //aa.ShowSuccessToast = Toast.ShowSuccessToast;
        //aa.ShowWarningToast = Toast.ShowWarningToast;
        if (RemoveToastsOnNavigation)
        {
            NavigationManager!.LocationChanged += ClearToasts;
        }
    }
    public void RemoveToast(Guid toastId)
    {
        InvokeAsync(() =>
        {
            var toastInstance = ToastList.SingleOrDefault(x => x.ID == toastId);
            ToastList.RemoveSpecificItem(toastInstance!);
            StateHasChanged();
        });
    }
    private void ClearToasts(object? sender, LocationChangedEventArgs args)
    {
        InvokeAsync(() =>
        {
            ToastList.Clear();
            StateHasChanged();
        });
    }
    internal BasicList<ToastInstance> ToastList { get; set; } = new BasicList<ToastInstance>();
    internal void ShowToast(EnumToastLevel level, string message)
    {
        ShowToast(level, builder => builder.AddContent(0, message));
    }
    private void ShowToast(EnumToastLevel level, RenderFragment message)
    {
        InvokeAsync(() =>
        {
            var settings = BuildToastSettings(level, message, "");
            var toast = new ToastInstance(Guid.NewGuid(), DateTime.Now, settings);
            ToastList.Add(toast);
            StateHasChanged();
        });
    }
}