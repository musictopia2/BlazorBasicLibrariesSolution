using CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.Misc;
using aa2 = BasicBlazorLibrary.Components.CssGrids.RowColumnHelpers;

namespace BasicBlazorLibrary.Components.CssGrids;

public partial class GridSeveralColumnsUnlimited
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
    [Parameter]
    public bool Inline { get; set; }
    [Parameter]
    public string RowGap { get; set; } = "";
    [Parameter]
    public string ColumnGap { get; set; } = "";

    [Parameter]
    public bool StopClickPropagation { get; set; } = false;

    [Parameter]
    public string ColumnLength { get; set; } = $"{aa2.OneSpread}";
    [Parameter]
    public string Class { get; set; } = ""; //so you can add other parts to this.
    [Parameter]
    public string Style { get; set; } = "";
    
    [Parameter]
    public int HowManyColumns { get; set; }

    private string GetSeveralColumns()
    {
        StrCat cats = new();
        HowManyColumns.Times(x =>
        {
            cats.AddToString(ColumnLength, " ");
        });
        return cats.GetInfo();

    }
}