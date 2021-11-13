namespace BasicBlazorLibrary.Components.BaseClasses;
public abstract class KeyComponentBase : ComponentBase
{
    protected static string GetKey => Guid.NewGuid().ToString();
}