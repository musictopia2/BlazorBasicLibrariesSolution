using BasicBlazorLibrary.Components.BaseClasses;
namespace BasicBlazorLibrary.Components.PageStyles;
public class WhiteFullPage : FullPageComponentBase
{
    public override string BackgroundColor => cc1.White.ToWebColor;
    public override string TextColor => cc1.Black.ToWebColor;
}