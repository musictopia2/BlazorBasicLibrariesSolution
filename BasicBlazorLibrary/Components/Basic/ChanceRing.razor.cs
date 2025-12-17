namespace BasicBlazorLibrary.Components.Basic;
public partial class ChanceRing
{
    [Parameter]
    public double PercentChance { get; set; }
    [Parameter]
    public double StartMedium { get; set; } = 30;
    [Parameter]
    public double StartHigh { get; set; } = 65;
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
        Circumference * (1 - (PercentChance / 100.0));
    private string ProgressColor
    {
        get
        {
            if (PercentChance == 0)
            {
                return BaseColor;
            }
            if (PercentChance <= StartMedium)
            {
                return LowChanceColor;
            }
            if (PercentChance <= StartHigh)
            {
                return MediumChanceColor;
            }
            return HighChanceColor;
        }
    }
    protected override void OnParametersSet()
    {
        PercentChance = Math.Clamp(PercentChance, 0, 100);

        StartMedium = Math.Clamp(StartMedium, 0, 100);
        StartHigh = Math.Clamp(StartHigh, 0, 100);

        if (StartMedium > StartHigh)
        {
            (StartMedium, StartHigh) = (StartHigh, StartMedium);
        }
    }
}