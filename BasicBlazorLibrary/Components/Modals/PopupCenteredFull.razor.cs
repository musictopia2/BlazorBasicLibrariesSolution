using BasicBlazorLibrary.Components.MediaQueries.ParentClasses;
namespace BasicBlazorLibrary.Components.Modals;
public partial class PopupCenteredFull
{
    [Parameter]
    public string Width { get; set; } = "40vmin";
    [CascadingParameter]
    private MediaQueryListComponent? Media { get; set; }
    protected override bool ProtectedHiddenFull
    {
        get
        {
            if (Media == null)
            {
                return base.ProtectedHiddenFull;
            }
            if (Media.DeviceCategory == EnumDeviceCategory.Phone)
            {
                return true;
            }
            return base.ProtectedHiddenFull;
        }
    }
    protected override string GetWidth => Width;
    private ElementReference? _element;
    private CenterClass? _center;
    protected override void OnInitialized()
    {
        _element = null;
        _center = new CenterClass(JS!);
        base.OnInitialized();
    }
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (Media is not null && Media.DeviceCategory == EnumDeviceCategory.Phone)
        {
            return;
        }
        if (FullScreen == false && DisableParentClickThrough == false)
        {
            await _center!.CenterDiv(_element);
        }
    }
}