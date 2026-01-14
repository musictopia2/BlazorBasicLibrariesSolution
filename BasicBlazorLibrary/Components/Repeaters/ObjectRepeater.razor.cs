namespace BasicBlazorLibrary.Components.Repeaters;

public partial class ObjectRepeater<T>
{
    
    [Parameter]
    public RenderFragment<T>? ChildContent { get; set; }

    [Parameter]
    [EditorRequired]
    public IEnumerable<T> RenderList { get; set; }

}