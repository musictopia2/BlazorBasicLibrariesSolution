﻿namespace BasicBlazorLibrary.BasicJavascriptClasses;
public class CenterClass : BaseLibraryJavascriptClass //can eliminate after getting javascript component isolation working.
{
    public CenterClass(IJSRuntime js) : base(js)
    {
    }
    protected override string JavascriptFileName => "center";
    public async Task CenterDiv(ElementReference? element)
    {
        await ModuleTask.InvokeVoidFromClassAsync("center", element);
    }
}