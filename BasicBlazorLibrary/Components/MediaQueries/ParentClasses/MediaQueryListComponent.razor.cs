using BasicBlazorLibrary.Components.MediaQueries.ResizeHelpers;
using System.Runtime.CompilerServices;

namespace BasicBlazorLibrary.Components.MediaQueries.ParentClasses;
public partial class MediaQueryListComponent : IAsyncDisposable
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
    [Inject]
    private IJSRuntime? JS { get; set; }
    private ResizeListenerClass? _resize;
    public EnumScreenOrientation ScreenOrientation { get; private set; }
    public EnumDeviceSize DeviceSize { get; private set; }
    public EnumDeviceCategory DeviceCategory
    {
        get
        {
            return DeviceSize switch
            {
                EnumDeviceSize.SmallPhone => EnumDeviceCategory.Phone,
                EnumDeviceSize.LargePhone => EnumDeviceCategory.Phone,
                EnumDeviceSize.SmallTablet => EnumDeviceCategory.Tablet,
                EnumDeviceSize.LargeTablet => EnumDeviceCategory.Tablet,
                EnumDeviceSize.Desktop => EnumDeviceCategory.Desktop,
                _ => throw new CustomBasicException("Nothing found"),
            };
        }
    }
    public BrowserSize? BrowserInfo { get; private set; }
    private bool OperatingSystemContainsKeyboard { get; set; }
    public bool HasKeyboard(EnumKeyboardCategory category)
    {
        switch (category)
        {
            case EnumKeyboardCategory.Real:
                return OperatingSystemContainsKeyboard;
            case EnumKeyboardCategory.Emulation:
                if (DeviceSize == EnumDeviceSize.Desktop)
                {
                    return true;
                }
                return false;
            case EnumKeyboardCategory.ManuelKeyboard:
                return true;
            case EnumKeyboardCategory.ManuelTouchscreen:
                return false;
            default:
                return false;
        }
    }
    private bool _loading = true;
    protected override void OnInitialized()
    {
        _resize = new (JS!);
        _resize.Onresized = MediaQueryListComponent_OnResized;
    }
    protected override async Task OnInitializedAsync()
    {
        try
        {
            OperatingSystemContainsKeyboard = await JS!.HasKeyboard();
            await _resize!.InitAsync();
        }
        catch (Exception)
        {
            return;
        }
    }
    public event Action? SizeChangedEvent;
    private void MediaQueryListComponent_OnResized(BrowserSize obj)
    {
        BrowserInfo = obj;

        var w = obj.Width;
        var h = obj.Height;

        ScreenOrientation = (h > w)
            ? EnumScreenOrientation.Portrait
            : EnumScreenOrientation.Landscape;

        var smallest = Math.Min(w, h);

        DeviceSize = smallest switch
        {
            >= 900 => EnumDeviceSize.Desktop,
            >= 700 => EnumDeviceSize.LargeTablet,
            >= 600 => EnumDeviceSize.SmallTablet,
            >= 420 => EnumDeviceSize.LargePhone,
            _ => EnumDeviceSize.SmallPhone
        };

        _loading = false;
        InvokeAsync(StateHasChanged);
        SizeChangedEvent?.Invoke(); //this time, we need events because more than one may be needed.  this seems to be the best way to handle this.
        //this is used when we don't care about the height and widths of the entire screen but may need to know the size of something within it.
        //if we do care about the heights and widths, then can use it as well.
        //this is just an option (not sure if i need or not).
        //did include new extensions so a person has choices of what to use.
    }
    public ValueTask DisposeAsync()
    {
        return ValueTask.CompletedTask;
    }
}