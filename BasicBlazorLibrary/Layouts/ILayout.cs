using CommonBasicLibraries.BasicUIProcesses;
namespace BasicBlazorLibrary.Layouts;
public interface ILayout : IExit, ISystemError, IMessageBox
{
    StartLayout? Layout { get; set; }
}