using BasicBlazorLibrary.Components.MediaQueries.ParentClasses;
using aa1 = BasicBlazorLibrary.Components.CssGrids.Helpers;
namespace BasicBlazorLibrary.Components.MediaQueries.MediaListUseClasses;
public partial class FlexibleOrientationComponent
{
    [CascadingParameter]
    private MediaQueryListComponent? MediaList { get; set; } //anybody can use it if needed anyways.
    [Parameter]
    public RenderFragment? MainContent { get; set; }
    [Parameter]
    public RenderFragment? SideContent { get; set; }
    [Parameter]
    public RenderFragment? HeaderContent { get; set; }
    private string GetColumns
    {
        get
        {
            if (HeaderContent == null)
            {
                return aa1.RepeatMaximum(2);
            }
            return $"{aa1.RepeatMaximum(1)} {aa1.RepeatSpreadOut(1)}";
        }
    }
    private string GetRows(bool horizontal)
    {
        if (horizontal == false)
        {
            if (HeaderContent == null)
            {
                return aa1.RepeatAuto(2);
            }
            return aa1.RepeatAuto(3);
        }
        return "auto";
    }
}