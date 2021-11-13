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
    private async Task CenterDivAsync()
    {
        await _reference!.InvokeVoidAsync("center", _element);
    }
    private IJSObjectReference? _reference;
    protected override string GetWidth => Width;
    private ElementReference? _element;
    //private CenterClass? _center;
    protected override void OnInitialized()
    {
        _element = null;
        //eventually see if i can do source generators to possibly create this or even have strongly typed methods (?)
        _reference = JS!.InvokeAsync<IJSObjectReference>("import", "./_content/BasicBlazorLibrary.Components.Modals/PopupCenteredFull.razor.js").Result;
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
            await CenterDivAsync();
        }
    }
}