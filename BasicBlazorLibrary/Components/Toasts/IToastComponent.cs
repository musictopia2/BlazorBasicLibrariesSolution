using CommonBasicLibraries.BasicUIProcesses;
namespace BasicBlazorLibrary.Components.Toasts;
public interface IToastComponent : IToast
{
    BlazoredToasts? Toast { get; set; }
}