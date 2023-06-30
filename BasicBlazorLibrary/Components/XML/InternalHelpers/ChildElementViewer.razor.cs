namespace BasicBlazorLibrary.Components.XML.InternalHelpers;
public partial class ChildElementViewer
{
    private BasicList<XNode> GetChildren()
    {
        return Element!.Nodes().ToBasicList();
    }
}