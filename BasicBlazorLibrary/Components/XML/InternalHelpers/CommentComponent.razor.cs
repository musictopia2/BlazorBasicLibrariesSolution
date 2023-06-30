namespace BasicBlazorLibrary.Components.XML.InternalHelpers;
public partial class CommentComponent
{
    [Parameter]
    [EditorRequired]
    public XNode? Comment { get; set; }
    [Parameter]
    [EditorRequired]
    public int HowManyTabs { get; set; }
    private BasicList<string> GetList()
    {
        var value = Comment!.ToString();
        BasicList<string> output = value.Split(Constants.VBCrLf).ToBasicList();
        return output;
    }
}