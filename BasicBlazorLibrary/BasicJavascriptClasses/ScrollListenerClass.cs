namespace BasicBlazorLibrary.BasicJavascriptClasses;
public class ScrollListenerClass : BaseLibraryJavascriptClass
{
    public ScrollListenerClass(IJSRuntime js) : base(js)
    {
    }
    public event Action<int>? Scrolled;
    [JSInvokable]
    public void ScrollChanged(int value)
    {
        Scrolled?.Invoke(value);
    }
    public async Task InitAsync(ElementReference? element, bool isVertical = true)
    {
        if (element == null)
        {
            return; //just in case.
        }
        if (isVertical)
        {
            await ModuleTask.InvokeVoidFromClassAsync("starty", DotNetObjectReference.Create(this), element); //i am forced to use the dotnetobjectreferencecreate method 
        }
        else
        {

            await ModuleTask.InvokeVoidFromClassAsync("startx", DotNetObjectReference.Create(this), element);
        }
    }
    protected override string JavascriptFileName => "scrolllistener";
}