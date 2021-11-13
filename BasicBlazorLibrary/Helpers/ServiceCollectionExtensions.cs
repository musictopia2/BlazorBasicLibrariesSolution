using BasicBlazorLibrary.Components.Toasts;
using BasicBlazorLibrary.Layouts;
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
    public static IServiceCollection RegisterBlazorBeginningClasses(this IServiceCollection services)
    {
        services.AddScoped<IToastComponent, ToastService>(); //hopefully toastcomponent is enough (?)
        //services.AddScoped<IToast>(xx => xx.GetRequiredService<ToastService>());
        //services.AddScoped<IToastComponent>(xx => xx.GetRequiredService<ToastService>());
        if (bb.OS == BasicDataFunctions.EnumOS.WindowsDT)
        {
            return services; //nothing to register
        }
        services.AddScoped<ILayout, LayoutService>();
        return services;
    }
}