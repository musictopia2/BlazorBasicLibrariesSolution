using BasicBlazorLibrary.Components.Toasts;
using BasicBlazorLibrary.Layouts;
using CommonBasicLibraries.BasicDateTimeProcesses; //not common enough.
using CommonBasicLibraries.BasicUIProcesses;
using Microsoft.Extensions.DependencyInjection; //not common enough to put to globals
namespace BasicBlazorLibrary.Helpers;
public static class ServiceCollectionExtensions
{
    extension (IServiceCollection services)
    {
        //hopefully i can start using IServiceCollection instead of directly the ServiceCollection (?)
        //if not, then add back as another extension (?)
        public IServiceCollection RegisterRealDatePicker()
        {
            services.AddSingleton<IDatePicker, RealDatePicker>();
            services.AddSingleton<IDateOnlyPicker, RealDatePicker>();
            return services;
        }
        public IServiceCollection RegisterBlazorBeginningClasses()
        {
            services.AddScoped<ToastService>();
            services.AddScoped<IToast>(xx => xx.GetRequiredService<ToastService>());
            services.AddScoped<IToastComponent>(xx => xx.GetRequiredService<ToastService>());
            if (bb1.OS == BasicDataFunctions.EnumOS.WindowsDT)
            {
                return services; //nothing to register because windows does something different for them.
            }
            services.AddScoped<LayoutService>();
            services.AddScoped<ILayout>(xx => xx.GetRequiredService<LayoutService>());
            services.AddScoped<IExit>(xx => xx.GetRequiredService<LayoutService>());
            services.AddScoped<ISystemError>(xx => xx.GetRequiredService<LayoutService>());
            services.AddScoped<IMessageBox>(xx => xx.GetRequiredService<LayoutService>());
            //services.AddScoped<ILayout, LayoutService>();
            return services;
        }
    }
}