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

    // Public DisposeAsync for easier access
    public async ValueTask DisposeAsync()
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

        string fullPath;

        if (IsLocal)
        {
            fullPath = $"./{jsName}";
        }
        else
        {
            Assembly? aa = Assembly.GetAssembly(GetType());
            if (aa == null)
            {
                throw new CustomBasicException(
                    "You need an assmebly for this.  Otherwise, rethink");
            }

            string firsts = aa.FullName!;
            int index = firsts.IndexOf(", ");
            string ns = firsts.Substring(0, index);
            fullPath = $"./_content/{ns}/{jsName}";
        }

        ModuleTask = new(async () =>
        {
            try
            {
                return await js.InvokeAsync<IJSObjectReference>(
                    "import", fullPath);
            }
            catch (JSException ex)
            {
                throw new InvalidOperationException(
                    $"Could not import JavaScript module '{fullPath}'.", ex);
            }
        });
    }
}