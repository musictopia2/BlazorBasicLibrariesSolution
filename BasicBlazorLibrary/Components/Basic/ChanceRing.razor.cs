using System.Globalization;

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
    [Parameter] 
    public RenderFragment? ChildContent { get; set; }
    [Parameter]
    public EventCallback OnClick { get; set; }

    [Parameter] 
    public bool ShowBackground { get; set; } = false;

    // NEW: base ring color instead of hardcoding "#444"
    [Parameter] 
    public string BaseRingColor { get; set; } = cc1.Black.ToWebColor;

    // NEW: extra padding inside ring so content never touches the stroke
    // (0 = auto only, based on stroke width)
    [Parameter] 
    public double InnerPaddingPx { get; set; } = 0;


    [Parameter] 
    public double ContentOffsetXPx { get; set; } = 0;
    [Parameter]
    public double ContentOffsetYPx { get; set; } = 0;


    private string ContentTransformStyle =>
        $"transform: translate({ContentOffsetXPx}px, {ContentOffsetYPx}px);";
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


    // Parses "10px" -> 10. If parsing fails, use a sensible default.
    private double StrokeWidthPx => TryParsePx(StrokeWidth, 10);

    // Safe inset: half stroke (since stroke goes both inward/outward) + a little breathing room + optional extra
    private double ContentInsetPx => (StrokeWidthPx / 2.0) + 6 + InnerPaddingPx;

    private string ContentInsetStyle =>
        $"inset:{ContentInsetPx.ToString(CultureInfo.InvariantCulture)}px;";

    private static double TryParsePx(string value, double fallback)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return fallback;
        }

        value = value.Trim().ToLowerInvariant();
        if (value.EndsWith("px"))
        {
            value = value[..^2];
        }

        if (double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out var d))
            return d;

        return fallback;
    }


}