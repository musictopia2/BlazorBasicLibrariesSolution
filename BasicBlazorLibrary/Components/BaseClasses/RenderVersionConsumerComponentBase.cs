namespace BasicBlazorLibrary.Components.BaseClasses;
public abstract class RenderVersionDependentComponentBase : KeyComponentBase
{
    [Parameter, EditorRequired]
    public int RenderVersion { get; set; }
}