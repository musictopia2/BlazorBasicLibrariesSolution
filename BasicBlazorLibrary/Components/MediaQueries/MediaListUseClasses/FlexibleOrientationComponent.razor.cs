using BasicBlazorLibrary.Components.MediaQueries.ParentClasses;
using aa2 = BasicBlazorLibrary.Components.CssGrids.Helpers;
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
    [Parameter]
    public EnumSideLocation SideOrder { get; set; } = EnumSideLocation.Last;
    private string GetColumns
    {
        get
        {
            if (HeaderContent == null)
            {
                return aa2.RepeatMaximum(2);
            }
            if (SideOrder == EnumSideLocation.Last)
            {
                return $"{aa2.RepeatMaximum(1)} {aa2.RepeatSpreadOut(1)}";
            }
            return $"{aa2.RepeatSpreadOut(1)} {aa2.RepeatMaximum(1)}";
        }
    }
    private string GetRows(bool horizontal)
    {
        if (horizontal == false)
        {
            if (HeaderContent == null)
            {
                return aa2.RepeatAuto(2);
            }
            return aa2.RepeatAuto(3);
        }
        return "auto";
    }
}