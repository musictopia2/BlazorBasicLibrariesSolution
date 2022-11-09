using System.Text;
namespace BasicBlazorLibrary.Components.CssGrids;
public partial class GridContainer
{
    [Parameter]
    public string Class { get; set; } = "";
    [Parameter]
    public string Style { get; set; } = "";
    /// <summary>
    /// Definition of the space between each row.  Examples: auto 80px
    /// </summary>
    [Parameter]
    public string RowGap { get; set; } = "";
    /// <summary>
    /// Definition of the space between each column.  Examples: auto 80px
    /// </summary>
    [Parameter]
    public string ColumnGap { get; set; } = "";
    [Parameter]
    public string Height { get; set; } = "";
    [Parameter]
    public string Width { get; set; } = "";
    /// <summary>
    /// If set to true then the layout of the grid will be "inline" instead of stretching to fill the container.
    /// </summary>
    /// <value>Default is false</value>
    [Parameter]
    public bool Inline { get; set; }
    /// <summary>
    /// Definition of the number and width of each column.  Examples: auto 80px
    /// </summary>
    [Parameter]
    public string Columns { get; set; } = "";
    /// <summary>
    /// Definition of the number and size height of each row.  Examples: auto 80px
    /// </summary>
    [Parameter]
    public string Rows { get; set; } = "";
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
    private string GetStyle()
    {
        StringBuilder sb = new ();
        if (Height != "")
        {
            sb.Append($"height: {Height};");
        }
        if (Width != "")
        {
            sb.Append($"width: {Width};");
        }
        if (Inline)
        {
            sb.Append("display: inline-grid;");
        }
        else
        {
            sb.Append("display: grid;");
        }
        if (ColumnGap != "")
        {
            sb.Append($"grid-column-gap: {ColumnGap};");
        }
        if (RowGap != "")
        {
            sb.Append($"grid-row-gap: {RowGap};");
        }

        if (Columns != "")
        {
            sb.Append($"grid-template-columns: {Columns};");
        }
        if (Rows != "")
        {
            sb.Append($"grid-template-rows: {Rows};");
        }
        return sb.ToString();
    }
}