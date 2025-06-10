namespace BasicBlazorLibrary.BasicJavascriptClasses;
public class CloseBrowserClass(IJSRuntime js) : BaseLibraryJavascriptClass(js)
{
    private DotNetObjectReference<CloseBrowserClass>? _dotNetRef;
    protected override string JavascriptFileName => "closebrowser.js";
    [JSInvokable]
    public async Task OnBrowserClosing()
    {
        if (Closed is null)
        {
            return;
        }
        await Closed.Invoke();
    }
    public Func<Task>? Closed { get; set; }
    public async Task DetectCloseAsync()
    {
        _dotNetRef = DotNetObjectReference.Create(this);
        await ModuleTask.InvokeVoidFromClassAsync("detectclose", _dotNetRef); //i am forced to use the dotnetobjectreferencecreate method 
    }
    protected override async ValueTask DisposeAsyncCore()
    {
        await ModuleTask.InvokeVoidFromClassAsync("removeclose");
        _dotNetRef?.Dispose();
        _dotNetRef = null;
        await base.DisposeAsyncCore();
    }
}