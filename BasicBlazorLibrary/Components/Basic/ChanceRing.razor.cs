namespace BasicBlazorLibrary.Components.Basic;
public partial class ChanceRing
{
    [Parameter]
    public int ChancePercent { get; set; }
    [Parameter]
    public int StartMedium { get; set; } = 30;
    [Parameter]
    public int StartHigh { get; set; } = 65;
    [Parameter]
    public string BaseColor { get; set; } = cc1.DarkGray.ToWebColor;
    [Parameter]
    public string LowChanceColor { get; set; } = cc1.DarkRed.ToWebColor;
    [Parameter]
    public string MediumChanceColor { get; set; } = cc1.Gold.ToWebColor;
    [Parameter]
    public string HighChanceColor { get; set; } = cc1.LimeGreen.ToWebColor;
    [Parameter]
    public string StrokeWidth { get; set; } = "10px";
    [Parameter]
    public string Size { get; set; } = "100px"; //can be as flexible as necessary.
    [Parameter]
    public string FillColor { get; set; } = cc1.Black.ToWebColor;
    private const double _radius = 50;
    private const double _center = 60; // center of 120x120 viewBox
    private static double Circumference => 2 * Math.PI * _radius;
    private double DashOffset =>
        Circumference * (1 - (ChancePercent / 100.0));
    private string ProgressColor
    {
        get
        {
            if (ChancePercent == 0)
            {
                return BaseColor;
            }
            if (ChancePercent <= StartMedium)
            {
                return LowChanceColor;
            }
            if (ChancePercent <= StartHigh)
            {
                return MediumChanceColor;
            }
            return HighChanceColor;
        }
    }
}