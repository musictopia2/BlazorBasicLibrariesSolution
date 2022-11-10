namespace BasicBlazorLibrary.Components.BaseClasses;
public abstract partial class FullPageComponentBase
{
    public abstract string BackgroundColor { get; }
    public abstract string TextColor { get; }
    //[CascadingParameter]
    //private MediaQueryListComponent? MediaQuery { get; set; }
    //private string Height => $"{MediaQuery!.BrowserInfo!.Height}px";
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
}