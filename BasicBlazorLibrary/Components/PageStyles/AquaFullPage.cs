using BasicBlazorLibrary.Components.BaseClasses;
namespace BasicBlazorLibrary.Components.PageStyles;
public class AquaFullPage : FullPageComponentBase
{
    public override string BackgroundColor => cc.Aqua.ToWebColor();
    public override string TextColor => cc.Navy.ToWebColor();
}