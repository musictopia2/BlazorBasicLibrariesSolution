namespace BasicBlazorLibrary.Components.CssManagement;
public partial class BaseCssLoader : CssSectionComponentBase
{
    protected override string SectionName => cc2.BaseCssSection;
    [Parameter]
    public bool UseAppCss { get; set; } //this is generic enough.
    //this way i can allow to make it easy to add just one css file.
    [Parameter]
    public string SingleCssFile { get; set; } = ""; //this is in addition to the app.css.  if you specify app.css, then show as false.
    private bool NeedsDefaultCss
    {
        get
        {
            if (UseAppCss == false)
            {
                return false;
            }
            if (SingleCssFile.Equals("app.css", StringComparison.CurrentCultureIgnoreCase))
            {
                return false;
            }
            return true;
        }
    }
}