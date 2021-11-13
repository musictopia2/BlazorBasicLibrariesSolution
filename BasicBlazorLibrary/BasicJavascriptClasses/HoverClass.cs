namespace BasicBlazorLibrary.BasicJavascriptClasses;
public class HoverClass : BaseLibraryJavascriptClass
{
    public HoverClass(IJSRuntime js) : base(js)
    {
    }
    protected override string JavascriptFileName => "hover.js";
    public async Task HoverAsync(ElementReference element, string source) //we reserve the right to use this from more than just one component.
    {
        await ModuleTask.InvokeVoidFromClassAsync("hover", element, source);
    }
    public async Task UnHoverAsync(ElementReference element, string source)
    {
        await ModuleTask.InvokeVoidFromClassAsync("unhover", element, source);
    }
}