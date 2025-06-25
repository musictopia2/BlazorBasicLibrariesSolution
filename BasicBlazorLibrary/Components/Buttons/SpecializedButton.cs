namespace BasicBlazorLibrary.Components.Buttons;
public class SpecializedButton : StyledButton
{
    [Parameter]
    [EditorRequired]
    public string CustomButtonClass { get; set; } = "";
    protected override string ButtonClass => CustomButtonClass;
}