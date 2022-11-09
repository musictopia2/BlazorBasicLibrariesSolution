using BasicBlazorLibrary.Components.BaseClasses;
namespace BasicBlazorLibrary.Components.PageStyles;
public class AquaFullPage : FullPageComponentBase
{
    public override string BackgroundColor => cc1.Aqua.ToWebColor();
    public override string TextColor => cc1.Navy.ToWebColor();
}