namespace BasicBlazorLibrary.BasicJavascriptClasses;
public class SimpleCarouselClass : BaseLibraryJavascriptClass //temporary until i figure out how to get javascript component isolation working.
{
    public SimpleCarouselClass(IJSRuntime js) : base(js)
    {
    }
    protected override string JavascriptFileName => "simplecarousel";
    public async Task StartAsync()
    {
        await ModuleTask.InvokeVoidFromClassAsync("start");
    }
}
