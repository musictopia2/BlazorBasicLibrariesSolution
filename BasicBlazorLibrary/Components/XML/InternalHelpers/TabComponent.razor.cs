namespace BasicBlazorLibrary.Components.XML.InternalHelpers;
public partial class TabComponent
{
    [Parameter]
    public int HowManyTabs { get; set; }
    private string GetSpaces()
    {
        int maxs = HowManyTabs * 4;
        //string output = "";
        StringBuilder builds = new();
        maxs.Times(() =>
        {
            builds.Append("&nbsp;");
        });
        return builds.ToString();
    }
}