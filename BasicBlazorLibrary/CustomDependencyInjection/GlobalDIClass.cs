namespace BasicBlazorLibrary.CustomDependencyInjection;
internal static class GlobalDIClass
{
    internal static IServiceProvider? NewProvider { get; set; }
    //several processes can go ahead and register the services.
    //unless i had a custom bootstrap class (?)
}