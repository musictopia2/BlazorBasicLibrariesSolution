namespace BasicBlazorLibrary.Components.CssManagement;
public partial class BaseCssLoader : CssSectionComponentBase
{
    protected override string SectionName => cc2.BaseCssSection;
    //this way i can allow to make it easy to add just one css file.
}