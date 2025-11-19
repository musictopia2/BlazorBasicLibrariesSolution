namespace BasicBlazorLibrary.Components.Basic;
public class ReaderModel
{
    public string HighlightColor { get; set; } = cc1.Aqua.ToWebColor; 
    public int ElementHighlighted { get; internal set; } = 0;
    public int ElementScrollTo { get; set; }
    public bool ScrollVisible { get; set; } = true;
    public void Reset()
    {
        ElementScrollTo = 0;
        ElementHighlighted = 0; //there are cases especially when navigating where i have to reset.  since this is not doing automatically.
    }
}