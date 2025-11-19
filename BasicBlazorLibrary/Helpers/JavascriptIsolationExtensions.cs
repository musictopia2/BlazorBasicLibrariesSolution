namespace BasicBlazorLibrary.Helpers;
public static class JavascriptIsolationExtensions //sometimes i need this as well.
{
    private static string GetJsName(string name)
    {
        string jsName = name;
        if (jsName.EndsWith("js") == false)
        {
            jsName = $"{jsName}.js";
        }
        return jsName;
    }
    extension (IJSRuntime js)
    {
        public Lazy<Task<IJSObjectReference>> GetLocalModuleTask(string javascriptfile)
        {
            return js.GetModuleTask($"./{GetJsName(javascriptfile)}");
        }
        public Lazy<Task<IJSObjectReference>> GetLibraryModuleTask(string libraryName, string javascriptfile)
        {
            return js.GetModuleTask($"./_content/{libraryName}/{GetJsName(javascriptfile)}");
        }
        internal Lazy<Task<IJSObjectReference>> GetLibraryModuleTask(string javascriptfile)
        {
            return js.GetModuleTask($"./_content/BasicBlazorLibrary/{GetJsName(javascriptfile)}");
        }
        private Lazy<Task<IJSObjectReference>> GetModuleTask(string fullPath)
        {
            Lazy<Task<IJSObjectReference>> output = new(() => js.InvokeAsync<IJSObjectReference>(
             "import", fullPath).AsTask());
            return output;
        }
    }
    extension(Lazy<Task<IJSObjectReference>> moduleTask)
    {
        public async Task InvokeVoidDisposeAsync(string identifier, params object?[] args)
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync(identifier, args);
            await module.DisposeAsync();
        }
        public async Task<T> InvokeDisposeAsync<T>(string identifier, params object?[] args)
        {
            var module = await moduleTask.Value;
            var output = await module.InvokeAsync<T>(identifier, args);
            await module.DisposeAsync();
            return output;
        }
        public async Task InvokeVoidFromClassAsync(string identifier, params object?[] args)
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync(identifier, args);
        }
        public async Task<T> InvokeFromClassAsync<T>(string identifier, params object?[] args)
        {
            var module = await moduleTask.Value;
            var output = await module.InvokeAsync<T>(identifier, args);
            return output;
        }
    }
    
}