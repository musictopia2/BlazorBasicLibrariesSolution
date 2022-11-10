namespace BasicBlazorLibrary.Components.Basic;
public partial class BasicStyleComponent
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
    public const string Btn = "btn"; //this will help with intellisense for different class names.
    public const string NoSelect = "usernoselect";
    public const string TestRed = "testred";
    public const string Bold = "bold";
}