namespace BasicBlazorLibrary.Components.BaseClasses;
public abstract class FixedComponentBase : ComponentBase
{
    protected override bool ShouldRender()
    {
        return false;
    }
}