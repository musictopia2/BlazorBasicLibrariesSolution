namespace BasicBlazorLibrary.Helpers;
public static class StorageExtensions
{
    extension (IJSRuntime js)
    {
        public async Task StorageSetItemAsync(string key, object value)
        {

            string temps = jj1.SerializeObject(value);
            await js.InvokeVoidAsync("localStorage.setItem", key, temps);
        }
        public async Task StorageSetStringAsync(string key, string value)
        {
            await js.InvokeVoidAsync("localStorage.setItem", key, value);
        }
        public async Task<string> StorageGetStringAsync(string key)
        {
            string output = await js.InvokeAsync<string>("localStorage.getItem", key);
            return output;
        }
        public async Task<T> StorageGetItemAsync<T>(string key)
        {
            var serialisedData = await js.InvokeAsync<string>("localStorage.getItem", key);
            if (string.IsNullOrWhiteSpace(serialisedData))
            {
                return default!;
            }
            return jj1.DeserializeObject<T>(serialisedData);
            //return JsonConvert.DeserializeObject<T>(serialisedData)!;
        }
        public async Task StorageClearAsync()
        {
            await js.InvokeVoidAsync("localStorage.clear");
        }
        public async Task StorageRemoveItemAsync(string key)
        {
            await js.InvokeVoidAsync("localStorage.removeItem", key);
        }
        public bool ContainsKey(string key)
        {
            if (js is not IJSInProcessRuntime exts)
            {
                throw new CustomBasicException("js runtime not available.  if server side blazor is being used, then must use async version of javascript functions.");
            }
            return exts.Invoke<bool>("localStorage.hasOwnProperty", key);
        }
        public async Task<bool> ContainsKeyAsync(string key)
        {
            return await js.InvokeAsync<bool>("localStorage.hasOwnProperty", key);
        }
    }   
}