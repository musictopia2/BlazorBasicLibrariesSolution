namespace BasicBlazorLibrary.Components.BaseClasses;
public abstract class JavascriptComponentBase : ComponentBase
{
    [Inject]
    public IJSRuntime? JS { get; set; }
}