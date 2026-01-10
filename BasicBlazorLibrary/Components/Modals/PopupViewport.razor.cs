using BasicBlazorLibrary.Components.Layouts;
using Microsoft.AspNetCore.Components.Web;

namespace BasicBlazorLibrary.Components.Modals;

public partial class PopupViewport
{
    [CascadingParameter] public OverlayInsets? Insets { get; set; }

    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Parameter]
    public bool TapToClose { get; set; } = false;


    [Parameter] public bool CanCloseAutomatically { get; set; } = true;
    [Parameter] public bool ShowHeaders { get; set; } = true;
    [Parameter] public string HeaderTitle { get; set; } = "";

    [Parameter] public int GapTopPx { get; set; } = 0;
    [Parameter] public int GapBottomPx { get; set; } = 0;
    [Parameter] public int GapLeftPx { get; set; } = 0;
    [Parameter] public int GapRightPx { get; set; } = 0;
    [Parameter] public EventCallback CloseButtonClick { get; set; }

    [Parameter] public string BackgroundColor { get; set; } = "white";

    private int EffectiveTop => (Insets?.TopPx ?? 0) + GapTopPx;
    private int EffectiveBottom => (Insets?.BottomPx ?? 0) + GapBottomPx;

    


    protected string HeaderStyle =>
        "flex:0 0 auto;" +
        "display:flex; align-items:center; justify-content:space-between;" +
        "padding:8px 12px;" +
        "border-bottom:1px solid #000;" +
        "user-select:none;";

    protected string CloseButtonStyle =>
        "font-size:2rem; font-weight:900;" +
        "border:0; background:transparent; cursor:pointer; line-height:1;";
    //attempt to change from overflow:auto; to hidden.  hopefully causes no other issues.
    //if it does, may need another parameter or something.
    protected string BodyStyle =>
        "flex:1 1 auto;" +
        "overflow:hidden;" +
        "box-sizing:border-box;" +
        "padding:8px 12px;";


    protected string RootOverlayStyle =>
    $"position:fixed;" +
    $"inset:0;" +                // <-- always full viewport
    $"z-index:{ZIndex};" +
    $"overflow:hidden;";

    protected string DimmerStyle =>
        "position:absolute;" +
        "inset:0;" +                 // <-- always full viewport
        "background-color: rgba(0,0,0,0.5);" +
        "z-index:0;";

   

    protected string SafeAreaStyle =>
        $"position:absolute;" +
        $"inset:{EffectiveTop}px {GapRightPx}px {EffectiveBottom}px {GapLeftPx}px;" +  // <-- nav/gaps apply here
        $"display:flex;" +
        $"justify-content:center;" +
        $"align-items:center;" +
        "user-select:none;" +
        $"z-index:1;" +               // <-- above dimmer
        $"overflow:hidden;";

    // If you want the panel to fill the safe area, keep 100%/100%.
    // If you want a centered box, use Width/Height params instead.
    protected string PanelStyle =>
        $"position:relative;" +
        $"width:100%; height:100%;" + // fills safe area
        $"box-sizing:border-box;" +
        $"background-color:{BackgroundColor};" +
        $"display:flex; flex-direction:column;" +
        $"overflow:hidden;";


    

    protected override async Task ClosePopupAsync()
    {
        if (CanCloseAutomatically == false)
        {
            return;
        }
        if (CloseButtonClick.HasDelegate)
        {
            await CloseButtonClick.InvokeAsync();
            return;
        }
        await base.ClosePopupAsync();
    }
}
