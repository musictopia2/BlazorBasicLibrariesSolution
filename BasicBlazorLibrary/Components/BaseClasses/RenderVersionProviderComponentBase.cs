namespace BasicBlazorLibrary.Components.BaseClasses;
public abstract class RenderVersionProviderComponentBase : KeyComponentBase
{
    protected int RenderVersion { get; private set; }
    protected override bool ShouldRender()
    {
        RenderVersion++;
        return true;
    }
}