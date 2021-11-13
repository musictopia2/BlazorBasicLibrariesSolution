namespace BasicBlazorLibrary.Components.Basic;
public class ReaderModel
{
    public string HighlightColor { get; set; } = cc.Aqua.ToWebColor(); 
    public int ElementHighlighted { get; internal set; } = 0;
    public int ElementScrollTo { get; set; }
    public bool ScrollVisible { get; set; } = true;
}