using BasicBlazorLibrary.Components.BaseClasses;
namespace BasicBlazorLibrary.Components.PageStyles;
public class NavyFullPage : FullPageComponentBase
{
    public override string BackgroundColor => cc.Navy.ToWebColor();
    public override string TextColor => cc.Aqua.ToWebColor();
}