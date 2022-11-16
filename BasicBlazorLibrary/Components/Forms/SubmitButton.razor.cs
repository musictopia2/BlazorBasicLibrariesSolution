namespace BasicBlazorLibrary.Components.Forms;
public partial class SubmitButton
{
    [CascadingParameter]
    public SubmitForm? Form { get; set; }

    [Parameter]
    public RenderFragment? ChildContent { get; set; }
    [Parameter]
    public bool IsPrimary { get; set; } = true; //if true, then use the special primary button.  usually will be primary button.
}