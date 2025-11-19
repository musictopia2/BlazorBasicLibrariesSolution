using BasicBlazorLibrary.Components.MediaQueries.ResizeHelpers; //not common enough to put under global usings.
namespace BasicBlazorLibrary.BasicJavascriptClasses;
public class ResizeListenerClass : BaseLibraryJavascriptClass
{
    public ResizeListenerClass(IJSRuntime js) : base(js) //needs this separately because uses 2 javascript files.
    {
    }
    protected override string JavascriptFileName => "Resize";
    public Action<BrowserSize>? Onresized { get; set; }
    public async Task InitAsync()
    {
        await ModuleTask.InvokeVoidFromClassAsync("listenForResize", DotNetObjectReference.Create(this)!);
    }
    /// <summary>
    /// Invoked by jsInterop, use the OnResized delgate to subscribe.
    /// </summary>
    /// <param name="browserWindowSize"></param>
    [JSInvokable]
    public void RaiseOnResized(BrowserSize browserWindowSize) =>
        Onresized?.Invoke(browserWindowSize);
}