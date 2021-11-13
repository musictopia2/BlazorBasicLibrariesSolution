namespace BasicBlazorLibrary.Components.BaseClasses;
public abstract partial class ButtonComponentBase
{
    public abstract string BackColor { get; }
    public abstract string TextColor { get; }
    public abstract string DisabledColor { get; }
    [Parameter]
    public EventCallback OnClick { get; set; }
    [Parameter]
    public EventCallback OnMouseOver { get; set; }

    [Parameter]
    public EventCallback OnMouseOut { get; set; }
    [Parameter]
    public bool IsEnabled { get; set; } = true;
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
    [Parameter]
    public bool Visible { get; set; } = true;
    public virtual string FontSize => "";
    public virtual string RightSpacing => "5px";
}
