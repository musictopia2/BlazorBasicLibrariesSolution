using Microsoft.Extensions.DependencyInjection;
namespace BasicBlazorLibrary.CustomDependencyInjection;
public abstract class CustomBootstrapperBase
{
    public CustomBootstrapperBase()
    {
        ServiceCollection services = new();
        Configure(services);
        RegisterAdditionalServices?.Invoke(services); //this means can transfer anything else over.
        GlobalDIClass.NewProvider = services.BuildServiceProvider();
        FinishInit?.Invoke();
    }
    public static Action<IServiceCollection>? RegisterAdditionalServices { get; set; }
    public static Action? FinishInit { get; set; }
    public abstract void Configure(ServiceCollection services);
}