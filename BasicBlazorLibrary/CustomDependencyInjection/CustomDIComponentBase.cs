using CommonBasicLibraries.BasicUIProcesses; //iffy.
namespace BasicBlazorLibrary.CustomDependencyInjection;
//try to do without reflection eventually
public abstract class CustomDIComponentBase : ComponentBase
{
    protected virtual bool RequireServices { get; } = true;
    protected virtual bool RequiresAtLeastOne { get; } = false;
    public CustomDIComponentBase()
    {
        RunProcesses();
    }
    private void RunProcesses()
    {
        if (GlobalDIClass.NewProvider is null)
        {
            throw new CustomBasicException("Did not create a new provider");
        }
        Type tt = GetType();
        BasicList<PropertyInfo> pp = tt.GetAllPropertiesWithAttribute<CustomInjectAttribute>().ToBasicList();
        if (RequiresAtLeastOne && pp.Count == 0)
        {
            UIPlatform.ShowSystemError("Required at least one custom inject service.  However, found none.  If on android, requires the property to be public");
            return;
        }
        foreach (var item in pp)
        {
            var service = GlobalDIClass.NewProvider.GetService(item.PropertyType);
            if (service is not null)
            {
                item.SetValue(this, service); //this means if not registered, i have to handle the errors later so i can get custom details.
            }
            if (RequireServices && service is null)
            {
                throw new CustomBasicException($"Services was required.  However type {item.PropertyType.Name} was not registered");
            }
        }
    }

}