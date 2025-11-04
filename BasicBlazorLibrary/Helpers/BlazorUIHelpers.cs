using CommonBasicLibraries.BasicUIProcesses;
namespace BasicBlazorLibrary.Helpers;
public static class BlazorUIHelpers
{
    //i propose doing here.
    //this will cause issues even with game package.
    public static IToast? Toast { get; set; }
    public static ISystemError? SystemError { get; set; }
    public static IMessageBox? MessageBox { get; set; }
    public static IExit? Exit { get; set; }
}