namespace BasicBlazorLibrary.BasicJavascriptClasses;
internal class FocusClass(IJSRuntime js) : BaseLibraryJavascriptClass(js)
{
    protected override string JavascriptFileName => "selectalltext";
    public async Task FocusAsync(ElementReference? element)
    {
        if (element is null)
        {
            return;
        }
        await ModuleTask.InvokeVoidFromClassAsync("selectall", element);
    }
}