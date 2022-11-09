using BasicBlazorLibrary.Components.BaseClasses;
namespace BasicBlazorLibrary.Components.PageStyles;
public class BlackFullPage : FullPageComponentBase
{
    public override string BackgroundColor => cc1.Black.ToWebColor();
    public override string TextColor => cc1.Aqua.ToWebColor();
}