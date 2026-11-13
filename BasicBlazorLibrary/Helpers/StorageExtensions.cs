namespace BasicBlazorLibrary.Helpers;
public static class StorageExtensions
{
    extension (IJSRuntime js)
    {
        public async Task StorageSetItemAsync(string key, object value)
        {
            string temps = jj1.SerializeObject(value);

            try
            {
                await js.InvokeVoidAsync("localStorage.setItem", key, temps);
            }
            catch (JSException ex)
            {
                throw new InvalidOperationException(
                    $"Could not save local storage item '{key}'.", ex);
            }
        }
        public async Task StorageSetStringAsync(string key, string value)
        {
            try
            {
                await js.InvokeVoidAsync("localStorage.setItem", key, value);
            }
            catch (JSException ex)
            {
                throw new InvalidOperationException(
                    $"Could not save local storage item '{key}'.", ex);
            }
        }

        public async Task<string> StorageGetStringAsync(string key)
        {
            try
            {
                return await js.InvokeAsync<string>("localStorage.getItem", key);
            }
            catch (JSException ex)
            {
                throw new InvalidOperationException(
                    $"Could not read local storage item '{key}'.", ex);
            }
        }

        public async Task<T> StorageGetItemAsync<T>(string key)
        {
            string serialisedData = await js.StorageGetStringAsync(key);

            if (string.IsNullOrWhiteSpace(serialisedData))
            {
                return default!;
            }

            return jj1.DeserializeObject<T>(serialisedData);
        }

        public async Task StorageClearAsync()
        {
            try
            {
                await js.InvokeVoidAsync("localStorage.clear");
            }
            catch (JSException ex)
            {
                throw new InvalidOperationException(
                    "Could not clear local storage.", ex);
            }
        }

        public async Task StorageRemoveItemAsync(string key)
        {
            try
            {
                await js.InvokeVoidAsync("localStorage.removeItem", key);
            }
            catch (JSException ex)
            {
                throw new InvalidOperationException(
                    $"Could not remove local storage item '{key}'.", ex);
            }
        }

        public bool ContainsKey(string key)
        {
            if (js is not IJSInProcessRuntime exts)
            {
                throw new CustomBasicException(
                    "js runtime not available.  if server side blazor is being used, then must use async version of javascript functions.");
            }

            try
            {
                return exts.Invoke<bool>("localStorage.hasOwnProperty", key);
            }
            catch (JSException ex)
            {
                throw new InvalidOperationException(
                    $"Could not check for local storage item '{key}'.", ex);
            }
        }

        public async Task<bool> ContainsKeyAsync(string key)
        {
            try
            {
                return await js.InvokeAsync<bool>("localStorage.hasOwnProperty", key);
            }
            catch (JSException ex)
            {
                throw new InvalidOperationException(
                    $"Could not check for local storage item '{key}'.", ex);
            }
        }
    }   
}