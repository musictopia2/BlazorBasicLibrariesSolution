namespace BasicBlazorLibrary.BasicJavascriptClasses;
public class TextBoxHelperClass : BaseLibraryJavascriptClass
{
    public TextBoxHelperClass(IJSRuntime js) : base(js)
    {
    }
    protected override string JavascriptFileName => "text";
    [JSInvokable]
    public void KeyPress(string key)
    {
        OnKeyPress?.Invoke(key);
    }
    public async Task StartAsync(ElementReference? element)
    {
        if (element is null)
        {
            return;
        }
        await ModuleTask.InvokeVoidFromClassAsync("start", DotNetObjectReference.Create(this)!, element);
    }
    public event Action<string>? OnKeyPress;
    public async Task SetInitTextAsync(ElementReference? element, string value)
    {
        if (element is null)
        {
            return;
        }
        await ModuleTask.InvokeVoidFromClassAsync("setInitialText", element, value!);
    }
    public async Task ClearTextAsync(ElementReference? element)
    {
        if (element is null)
        {
            return;
        }
        await ModuleTask.InvokeVoidFromClassAsync("clearText", element);
    }
    public async Task SetNewValueAndHighlightAsync(ElementReference? element, string value, int startAt)
    {
        if (element is null)
        {
            return;
        }
        await ModuleTask.InvokeVoidFromClassAsync("setvalueandhighlight", element, value!, startAt);
    }
    public async Task SetNewValueAloneAsync(ElementReference? element, string value)
    {
        if (element is null)
        {
            return;
        }
        await ModuleTask.InvokeVoidFromClassAsync("setvaluealone", element, value!);
    }
    public async Task<string> GetValueAsync(ElementReference? element)
    {
        if (element is null)
        {
            throw new CustomBasicException("Element cannot be null.  Rethink");
        }
        return await ModuleTask.InvokeFromClassAsync<string>("getValue", element);
    }
}