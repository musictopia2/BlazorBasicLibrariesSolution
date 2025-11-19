namespace BasicBlazorLibrary.BasicJavascriptClasses;
internal class ClickInputHelperClass(IJSRuntime js) : BaseLibraryJavascriptClass(js) //had to keep because had 2 classes.  this is okay this time.
{
    public Action<int>? InputClicked;
    public Action? OtherClicked;
    protected override string JavascriptFileName => "elementselector";
    public async Task InitAsync()
    {
        await ModuleTask.InvokeVoidFromClassAsync("start", DotNetObjectReference.Create(this)!);
    }
    [JSInvokable]
    public void JsOtherClicked()
    {
        OtherClicked?.Invoke();
    }
    [JSInvokable]
    public void JsMainClicked(string id)
    {
        if (id == "")
        {
            OtherClicked?.Invoke();
            return;
        }
        bool rets = int.TryParse(id, out int tabindex);
        if (rets == false)
        {
            OtherClicked?.Invoke();
            return;
        }
        InputClicked?.Invoke(tabindex);
    }
}