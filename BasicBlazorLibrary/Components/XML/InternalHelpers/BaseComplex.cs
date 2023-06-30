namespace BasicBlazorLibrary.Components.XML.InternalHelpers;
public abstract class BaseComplex : RawElement
{
    [Parameter]
    [EditorRequired]
    public BasicList<XNode> Nodes { get; set; } = new();
    protected bool HasComments => Nodes.Any(x => x.NodeType == System.Xml.XmlNodeType.Comment);
}