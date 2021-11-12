using CommonBasicLibraries.BasicDateTimeProcesses; //not common enough.
using Microsoft.Extensions.DependencyInjection; //not common enough to put to globals
namespace BasicBlazorLibrary.Helpers;
public static class ServiceCollectionExtensions
{
    public static ServiceCollection RegisterRealDatePicker(this ServiceCollection services)
    {
        services.AddSingleton<IDatePicker, RealDatePicker>();
        services.AddSingleton<IDateOnlyPicker, RealDatePicker>();
        return services;
        //this means i can do mocks if necessary.
    }
    public static IServiceCollection RegisterRealDatePicker(this IServiceCollection services)
    {
        services.AddSingleton<IDatePicker, RealDatePicker>();
        services.AddSingleton<IDateOnlyPicker, RealDatePicker>();
        return services;
    }
}