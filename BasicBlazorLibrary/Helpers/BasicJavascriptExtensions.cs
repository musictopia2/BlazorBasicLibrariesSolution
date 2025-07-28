namespace BasicBlazorLibrary.Helpers;
public static class BasicJavascriptExtensions
{
    private static Lazy<Task<IJSObjectReference>> GetModuleTask(this IJSRuntime js)
    {
        return js.GetLibraryModuleTask("basichelpers");
    }
    public static async Task<int> GetContainerTop(this IJSRuntime js, ElementReference? element)
    {
        var moduleTask = js.GetModuleTask();
        double firstValue = await moduleTask.InvokeDisposeAsync<double>("getcontainertop", element);
        int results = (int)firstValue;
        return results;
    }
    public static async Task<int> GetContainerLeft(this IJSRuntime js, ElementReference? element)
    {
        var moduleTask = js.GetModuleTask();
        double firstValue = await moduleTask.InvokeDisposeAsync<double>("getcontainerleft", element);
        int results = (int)firstValue;
        return results;
    }
    public static async Task OpenFullScreen(this IJSRuntime js)
    {
        var task = js.GetLibraryModuleTask("fullscreen");
        await task.InvokeVoidDisposeAsync("openFullscreen");
    }
    public static async Task ExitFullScreen(this IJSRuntime js)
    {
        var task = js.GetLibraryModuleTask("fullscreen");
        await task.InvokeVoidDisposeAsync("exitFullscreen");
    }
    public static async Task<int> GetContainerHeight(this IJSRuntime js, ElementReference? element)
    {
        var moduleTask = js.GetModuleTask();
        return await moduleTask.InvokeDisposeAsync<int>("getcontainerheight", element);
    }
    public static async Task<int> GetContainerWidth(this IJSRuntime js, ElementReference? element)
    {
        var moduleTask = js.GetModuleTask();
        return await moduleTask.InvokeDisposeAsync<int>("getcontainerwidth", element);
    }
    public static async Task<int> GetParentHeight(this IJSRuntime js, ElementReference? element)
    {
        var moduleTask = js.GetModuleTask();
        return await moduleTask.InvokeDisposeAsync<int>("getparentHeight", element);
    }
    public static async Task<int> GetParentWidth(this IJSRuntime js, ElementReference? element)
    {
        var moduleTask = js.GetModuleTask();
        return await moduleTask.InvokeDisposeAsync<int>("getparentWidth", element);
    }
    public static async Task<int> GetBrowserHeight(this IJSRuntime js)
    {
        var moduleTask = js.GetModuleTask();
        return await moduleTask.InvokeDisposeAsync<int>("getbrowserheight");
    }
    public static async Task<int> GetBrowserWidth(this IJSRuntime js)
    {
        var moduleTask = js.GetModuleTask();
        return await moduleTask.InvokeDisposeAsync<int>("getbrowserwidth");
    }
    public static async Task<int> PixelsPerRem(this IJSRuntime js)
    {
        var moduleTask = js.GetModuleTask();
        return await moduleTask.InvokeDisposeAsync<int>("convertRemToPixels", 1);
    }
    public static async Task<string> GetOperatingSystemAsync(this IJSRuntime js)
    {
        var module = js.GetLibraryModuleTask("operatingsystemhelpers");
        string results = await module.InvokeDisposeAsync<string>("getOS");
        return results;
    }
    public static async Task<bool> HasKeyboard(this IJSRuntime js)
    {
        var module = js.GetLibraryModuleTask("operatingsystemhelpers");
        bool output = await module.InvokeDisposeAsync<bool>("hasKeyboard");
        return output;
    }
    public static async Task NavigateToOnAnotherTabAsync(this IJSRuntime js, string url)
    {
        await js.InvokeVoidAsync("open", url, "_blank");
    }
    public static async Task ScrollToTopAsync(this IJSRuntime js)
    {
        var moduleTask = js.GetModuleTask();
        await moduleTask.InvokeVoidDisposeAsync("scrollToTopWindow");
    }
    public static async Task ScrollToTopAsync(this IJSRuntime js, ElementReference? element)
    {
        var moduleTask = js.GetModuleTask();
        await moduleTask.InvokeVoidDisposeAsync("scrollToTopElement", element);
    }
    public static async Task RefreshBrowser(this IJSRuntime js)
    {
        var moduleTask = js.GetModuleTask();
        await moduleTask.InvokeVoidDisposeAsync("refreshbrowser");
    }
    public static async Task Update(this IJSRuntime js)
    {
        var moduleTask = js.GetModuleTask();
        await moduleTask.InvokeVoidDisposeAsync("update");
    }
    public static async Task CopyTextAsync(this IJSRuntime js, string text)
    {
        var moduleTask = js.GetLibraryModuleTask("clipboard");
        await moduleTask.InvokeVoidFromClassAsync("clipboardCopy", text);
    }
}