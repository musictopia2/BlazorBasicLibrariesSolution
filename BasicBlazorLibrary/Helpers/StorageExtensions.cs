﻿using Newtonsoft.Json; //not common enough.  eventually can use system.json but not yet.
namespace BasicBlazorLibrary.Helpers;
public static class StorageExtensions
{
    public static async Task StorageSetItemAsync(this IJSRuntime js, string key, object value)
    {
        JsonSerializerSettings settings = new()
        {
            Formatting = Formatting.Indented
        };
        string temps = JsonConvert.SerializeObject(value, settings);
        await js.InvokeVoidAsync("localStorage.setItem", key, temps);
    }
    public static async Task StorageSetStringAsync(this IJSRuntime js, string key, string value)
    {
        await js.InvokeVoidAsync("localStorage.setItem", key, value);
    }
    public static async Task<string> StorageGetStringAsync(this IJSRuntime js, string key)
    {
        string output = await js.InvokeAsync<string>("localStorage.getItem", key);
        return output;
    }
    public static async Task<T> StorageGetItemAsync<T>(this IJSRuntime js, string key)
    {
        var serialisedData = await js.InvokeAsync<string>("localStorage.getItem", key);
        if (string.IsNullOrWhiteSpace(serialisedData))
        {
            return default!;
        }
        return JsonConvert.DeserializeObject<T>(serialisedData)!;
    }
    public static async Task StorageClearAsync(this IJSRuntime js)
    {
        await js.InvokeVoidAsync("localStorage.clear");
    }
    public static async Task StorageRemoveItemAsync(this IJSRuntime js, string key)
    {
        await js.InvokeVoidAsync("localStorage.removeItem", key);
    }
    public static bool ContainsKey(this IJSRuntime js, string key)
    {
        if (js is not IJSInProcessRuntime exts)
        {
            throw new CustomBasicException("js runtime not available.  if server side blazor is being used, then must use async version of javascript functions.");
        }
        return exts.Invoke<bool>("localStorage.hasOwnProperty", key);
    }
    public static async Task<bool> ContainsKeyAsync(this IJSRuntime js, string key)
    {
        return await js.InvokeAsync<bool>("localStorage.hasOwnProperty", key);
    }
}