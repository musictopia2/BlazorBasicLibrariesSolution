namespace BasicBlazorLibrary.BasicJavascriptClasses;
public class AutoScrollClass : BaseLibraryJavascriptClass
{
    public AutoScrollClass(IJSRuntime js) : base(js) { }
    protected override string JavascriptFileName => "autoscroll";
    public async Task ScrollToElementAsync(ElementReference? element)
    {
        if (element == null)
        {
            return;
        }
        await ModuleTask.InvokeVoidFromClassAsync("scrolltoelement", element);
    }
    public async Task ScrollToElementAsync(ElementReference? parent, int index)
    {
        if (parent == null)
        {
            return;
        }
        await ModuleTask.InvokeVoidFromClassAsync("scrolltoelementparent", parent, index);
    }
    public async Task SetScrollPosition(ElementReference? element, float position)
    {
        if (element is null)
        {
            return;
        }
        await ModuleTask.InvokeVoidFromClassAsync("setscroll", element, position);
    }
    public async Task<int> GetLeftScrollValue(ElementReference? element)
    {
        if (element is null)
        {
            return 0;
        }
        return await ModuleTask.InvokeFromClassAsync<int>("getscrollleftvalue", element);
    }
}