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
            Lazy<Task<IJSObjectReference>> output = new(async () =>
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

            return output;
        }
    }
    extension(Lazy<Task<IJSObjectReference>> moduleTask)
    {
        public async Task InvokeVoidDisposeAsync(
    string identifier, params object?[] args)
        {
            await using var module = await moduleTask.Value;

            try
            {
                await module.InvokeVoidAsync(identifier, args);
            }
            catch (JSException ex)
            {
                throw new InvalidOperationException(
                    $"JavaScript call '{identifier}' failed.", ex);
            }
        }

        public async Task<T> InvokeDisposeAsync<T>(
            string identifier, params object?[] args)
        {
            await using var module = await moduleTask.Value;

            try
            {
                return await module.InvokeAsync<T>(identifier, args);
            }
            catch (JSException ex)
            {
                throw new InvalidOperationException(
                    $"JavaScript call '{identifier}' failed.", ex);
            }
        }

        public async Task InvokeVoidFromClassAsync(
            string identifier, params object?[] args)
        {
            var module = await moduleTask.Value;

            try
            {
                await module.InvokeVoidAsync(identifier, args);
            }
            catch (JSException ex)
            {
                throw new InvalidOperationException(
                    $"JavaScript call '{identifier}' failed.", ex);
            }
        }

        public async Task<T> InvokeFromClassAsync<T>(
            string identifier, params object?[] args)
        {
            var module = await moduleTask.Value;

            try
            {
                return await module.InvokeAsync<T>(identifier, args);
            }
            catch (JSException ex)
            {
                throw new InvalidOperationException(
                    $"JavaScript call '{identifier}' failed.", ex);
            }
        }
    }
    
}