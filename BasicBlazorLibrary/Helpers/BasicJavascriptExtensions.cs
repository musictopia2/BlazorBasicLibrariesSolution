namespace BasicBlazorLibrary.Helpers;
public static class BasicJavascriptExtensions
{
    extension(IJSRuntime js)
    {
        private Lazy<Task<IJSObjectReference>> GetModuleTask()
        {
            return js.GetLibraryModuleTask("basichelpers");
        }
        public async Task<int> GetElementHeight(ElementReference? element)
        {
            CustomBasicException.ThrowIfNull(element);
            var moduleTask = js.GetModuleTask();
            double height = await moduleTask.InvokeDisposeAsync<double>(
                "getElementHeight", element);

            // ALWAYS round UP
            return (int)Math.Ceiling(height);
        }
        public async Task<int> GetContainerTop(ElementReference? element)
        {
            CustomBasicException.ThrowIfNull(element);
            var moduleTask = js.GetModuleTask();
            double firstValue = await moduleTask.InvokeDisposeAsync<double>("getcontainertop", element);
            int results = (int)firstValue;
            return results;
        }
        public async Task<int> GetContainerLeft(ElementReference? element)
        {
            CustomBasicException.ThrowIfNull(element);
            var moduleTask = js.GetModuleTask();
            double firstValue = await moduleTask.InvokeDisposeAsync<double>("getcontainerleft", element);
            int results = (int)firstValue;
            return results;
        }
        public async Task OpenFullScreen()
        {
            var task = js.GetLibraryModuleTask("fullscreen");
            await task.InvokeVoidDisposeAsync("openFullscreen");
        }
        public async Task ExitFullScreen()
        {
            var task = js.GetLibraryModuleTask("fullscreen");
            await task.InvokeVoidDisposeAsync("exitFullscreen");
        }
        public async Task<int> GetContainerHeight(ElementReference? element)
        {
            CustomBasicException.ThrowIfNull(element);
            var moduleTask = js.GetModuleTask();
            return await moduleTask.InvokeDisposeAsync<int>("getcontainerheight", element);
        }
        public async Task<int> GetContainerWidth(ElementReference? element)
        {
            CustomBasicException.ThrowIfNull(element);
            var moduleTask = js.GetModuleTask();
            return await moduleTask.InvokeDisposeAsync<int>("getcontainerwidth", element);
        }
        public async Task<int> GetParentHeight(ElementReference? element)
        {
            CustomBasicException.ThrowIfNull(element);
            var moduleTask = js.GetModuleTask();
            return await moduleTask.InvokeDisposeAsync<int>("getparentHeight", element);
        }
        public async Task<int> GetParentWidth(ElementReference? element)
        {
            CustomBasicException.ThrowIfNull(element);
            var moduleTask = js.GetModuleTask();
            return await moduleTask.InvokeDisposeAsync<int>("getparentWidth", element);
        }
        public async Task<int> GetBrowserHeight()
        {
            var moduleTask = js.GetModuleTask();
            return await moduleTask.InvokeDisposeAsync<int>("getbrowserheight");
        }
        public async Task<int> GetBrowserWidth()
        {
            var moduleTask = js.GetModuleTask();
            return await moduleTask.InvokeDisposeAsync<int>("getbrowserwidth");
        }
        public async Task<int> PixelsPerRem()
        {
            var moduleTask = js.GetModuleTask();
            return await moduleTask.InvokeDisposeAsync<int>("convertRemToPixels", 1);
        }
        public async Task<string> GetOperatingSystemAsync()
        {
            var module = js.GetLibraryModuleTask("operatingsystemhelpers");
            string results = await module.InvokeDisposeAsync<string>("getOS");
            return results;
        }
        public async Task<bool> HasKeyboard()
        {
            var module = js.GetLibraryModuleTask("operatingsystemhelpers");
            bool output = await module.InvokeDisposeAsync<bool>("hasKeyboard");
            return output;
        }
        public async Task NavigateToOnAnotherTabAsync(string url)
        {
            await js.InvokeVoidAsync("open", url, "_blank");
        }
        public async Task ScrollToTopAsync()
        {
            var moduleTask = js.GetModuleTask();
            await moduleTask.InvokeVoidDisposeAsync("scrollToTopWindow");
        }
        public async Task ScrollToTopAsync(ElementReference? element)
        {
            if (element is null)
            {
                return;
            }
            var moduleTask = js.GetModuleTask();
            await moduleTask.InvokeVoidDisposeAsync("scrollToTopElement", element);
        }
        public async Task RefreshBrowser()
        {
            var moduleTask = js.GetModuleTask();
            await moduleTask.InvokeVoidDisposeAsync("refreshbrowser");
        }
        public async Task Update()
        {
            var moduleTask = js.GetModuleTask();
            await moduleTask.InvokeVoidDisposeAsync("update");
        }
        public async Task CopyTextAsync(string text)
        {
            var moduleTask = js.GetLibraryModuleTask("clipboard");
            await moduleTask.InvokeVoidFromClassAsync("clipboardCopy", text!);
        }
    }
}