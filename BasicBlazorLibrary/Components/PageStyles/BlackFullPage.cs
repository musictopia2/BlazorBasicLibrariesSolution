using BasicBlazorLibrary.Components.BaseClasses;
namespace BasicBlazorLibrary.Components.PageStyles;
public class BlackFullPage : FullPageComponentBase
{
    public override string BackgroundColor => cc.Black.ToWebColor();
    public override string TextColor => cc.Aqua.ToWebColor();
}