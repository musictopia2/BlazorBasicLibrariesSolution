namespace BasicBlazorLibrary.Components.XML.InternalHelpers;
public abstract class RawElement : ComponentBase
{
    [Parameter]
    [EditorRequired]
    public XElement? Element { get; set; }
    [Parameter]
    [EditorRequired]
    public int HowManyTabs { get; set; }
    protected string OpeningName() => $"<{Element!.Name}>";
    protected string OnlyName() => $"<{Element!.Name} />";
    protected string StartName() => $"<{Element!.Name}";
    protected string ClosingName() => $"</{Element!.Name}>";
    protected int IncrementTabs => HowManyTabs + 1;
}