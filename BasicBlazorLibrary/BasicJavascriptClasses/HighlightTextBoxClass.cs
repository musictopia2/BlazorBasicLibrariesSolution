namespace BasicBlazorLibrary.BasicJavascriptClasses;
public class HighlightTextBoxClass(IJSRuntime js) : BaseLibraryJavascriptClass(js)
{
    protected override string JavascriptFileName => "highlighter";
    public async Task PartialHighlightText(ElementReference? element, int startat)
    {
        await ModuleTask.InvokeVoidFromClassAsync("highlighttext", element, startat);
    }
}
