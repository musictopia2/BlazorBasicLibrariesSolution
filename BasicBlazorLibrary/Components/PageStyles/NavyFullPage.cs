using BasicBlazorLibrary.Components.BaseClasses;
namespace BasicBlazorLibrary.Components.PageStyles;
public class NavyFullPage : FullPageComponentBase
{
    public override string BackgroundColor => cc1.Navy.ToWebColor;
    public override string TextColor => cc1.Aqua.ToWebColor;
}