namespace BasicBlazorLibrary.BasicJavascriptClasses;
public abstract class BaseStandardJavascriptClass : IAsyncDisposable
{
    protected Lazy<Task<IJSObjectReference>> ModuleTask;
    protected abstract bool IsLocal { get; }
    protected abstract string JavascriptFileName { get; }
    // The overridable dispose helper
    protected virtual async ValueTask DisposeAsyncCore()
    {
        if (ModuleTask.IsValueCreated)
        {
            var module = await ModuleTask.Value;
            await module.DisposeAsync();
        }
    }

    async ValueTask IAsyncDisposable.DisposeAsync()
    {
        await DisposeAsyncCore();
        GC.SuppressFinalize(this);
    }
    public BaseStandardJavascriptClass(IJSRuntime js)
    {
        string jsName = JavascriptFileName;
        if (jsName.EndsWith("js") == false)
        {
            jsName = $"{jsName}.js";
        }
        if (IsLocal)
        {
            ModuleTask = new(() => js.InvokeAsync<IJSObjectReference>(
            "import", $"./{jsName}").AsTask());
        }
        else
        {
            Assembly? aa = Assembly.GetAssembly(GetType());
            if (aa == null)
            {
                throw new CustomBasicException("You need an assmebly for this.  Otherwise, rethink");
            }
            string firsts = aa.FullName!;
            int index = firsts.IndexOf(", ");
            string ns = firsts.Substring(0, index);
            ModuleTask = new(() => js.InvokeAsync<IJSObjectReference>(
            "import", $"./_content/{ns}/{jsName}").AsTask());
        }
    }
}