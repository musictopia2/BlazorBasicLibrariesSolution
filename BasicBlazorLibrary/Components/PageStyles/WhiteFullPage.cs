using BasicBlazorLibrary.Components.BaseClasses;
namespace BasicBlazorLibrary.Components.PageStyles;
public class WhiteFullPage : FullPageComponentBase
{
    public override string BackgroundColor => cc.White.ToWebColor();
    public override string TextColor => cc.Black.ToWebColor();
}