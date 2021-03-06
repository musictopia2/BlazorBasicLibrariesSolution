using System.Text;
namespace BasicBlazorLibrary.Components.CssGrids;
public partial class GridItem
{
    [Parameter]
    public string Class { get; set; } = ""; //so you can add other parts to this.
    [Parameter]
    public string Style { get; set; } = "";
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
    /// <summary>
    /// Definition of the column to start in
    /// </summary>
    [Parameter]
    public int Column { get; set; } = 1;
    /// <summary>
    /// Definition of the columnSpan
    /// </summary>
    [Parameter]
    public int ColumnSpan { get; set; } = 1;
    /// <summary>
    /// Definition of the row
    /// </summary>
    [Parameter]
    public int Row { get; set; } = 1;
    /// <summary>
    /// Definition of the rowSpan
    /// </summary>
    [Parameter]
    public int RowSpan { get; set; } = 1;
    /// <summary>
    /// Horizontal alignement (Example: "@Justify.Center")
    /// </summary>
    [Parameter]
    public string HorizontalAlignment { get; set; } = "";
    /// <summary>
    /// Vertical alignemtn (Example:"@Justify.Center")
    /// </summary>
    [Parameter]
    public string VerticalAlignment { get; set; } = "";
    /// <summary>
    /// Control horizontal scrollbar- None, Show, Auto
    /// </summary>
    [Parameter]
    public string HorizontalScrollbar { get; set; } = "";
    /// <summary>
    /// Control vertical scrollbar- None, Show, Auto
    /// </summary>
    [Parameter]
    public string VerticalScrollbar { get; set; } = "";
    private string GetStyle()
    {
        StringBuilder sb = new();
        if (Column > 0)
        {
            if (ColumnSpan > 1)
            {
                sb.Append($"grid-column: {Column} / span {ColumnSpan};");
            }
            else
            {
                sb.Append($"grid-column: {Column};");
            }
        }
        if (Row > 0)
        {
            if (RowSpan > 1)
            {
                sb.Append($"grid-row: {Row} / span {RowSpan};");
            }
            else
            {
                sb.Append($"grid-row: {Row};");
            }
        }
        if (HorizontalAlignment != null)
        {
            sb.Append($"justify-content: {HorizontalAlignment}");
        }

        if (VerticalAlignment != null)
        {
            sb.Append($"align-content: {VerticalAlignment}");
        }
        sb.Append($"overflow-x: {HorizontalScrollbar ?? "hidden"};"); //i guess its okay this time.
        sb.Append($"overflow-y: {VerticalScrollbar ?? "hidden"};");
        if (Style != "")
        {
            sb.Append(Style);
        }
        return sb.ToString();
    }
}