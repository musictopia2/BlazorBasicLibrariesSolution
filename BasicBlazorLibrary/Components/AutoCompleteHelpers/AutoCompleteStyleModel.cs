namespace BasicBlazorLibrary.Components.AutoCompleteHelpers;
public class AutoCompleteStyleModel
{
    public string HighlightColor { get; set; } = cc1.Aqua.ToWebColor();
    public string HoverColor { get; set; } = cc1.LightGray.ToWebColor();
    public string HeaderTextColor { get; set; } = cc1.Black.ToWebColor();
    public string ComboTextColor { get; set; } = cc1.Black.ToWebColor();
    public string HeaderBackgroundColor { get; set; } = cc1.White.ToWebColor();
    public string ComboBackgroundColor { get; set; } = cc1.White.ToWebColor();
    public bool AllowDoubleClick { get; set; } = false;
    public string Width { get; set; } = "8vw";
    public string Height { get; set; } = "9vh";
    public string FontSize { get; set; } = "1rem";
    /// <summary>
    /// this is only used if virtualize so it knows the line height.  hint.  set to higher than fontsize or would get hosed.  this helps in margins.
    /// </summary>
    public string LineHeight { get; set; } = "1.5rem";
}