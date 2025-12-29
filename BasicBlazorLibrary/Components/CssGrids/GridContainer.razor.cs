namespace BasicBlazorLibrary.Components.CssGrids;
public partial class GridContainer
{
    [Parameter]
    public string Class { get; set; } = "";
    [Parameter]
    public string Style { get; set; } = "";
    [Parameter] 
    public bool ModalSafe { get; set; } = true;
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
    /// <summary>
    /// this would specify the same for both column and row.
    /// </summary>
    [Parameter] 
    public string Gap { get; set; } = "";
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
        StringBuilder sb = new();
        

        if (string.IsNullOrWhiteSpace(Height) == false)
        {
            sb.Append($"height: {Height};");
        }
        if (string.IsNullOrWhiteSpace(Width) == false)
        {
            sb.Append($"width: {Width};");
        }
        
        if (string.IsNullOrWhiteSpace(Gap) == false)
        {
            sb.Append($"gap: {Gap};");
        }
        else
        {
            if (string.IsNullOrWhiteSpace(ColumnGap) == false)
            {
                sb.Append($"column-gap: {ColumnGap};");
            }
            if (string.IsNullOrWhiteSpace(RowGap) == false)
            {
                sb.Append($"row-gap: {RowGap};");
            }
        }
        if (string.IsNullOrWhiteSpace(Columns) == false)
        {
            sb.Append($"grid-template-columns: {Columns};");
        }
        if (string.IsNullOrWhiteSpace(Rows) == false)
        {
            sb.Append($"grid-template-rows: {Rows};");
        }
        if (ModalSafe)
        {
            sb.Append("min-width: 0; min-height: 0; max-width: 100%; box-sizing: border-box; align-content: start; align-items: start;");
        }
        if (string.IsNullOrWhiteSpace(Style) == false)
        {
            sb.Append(Style.Trim().TrimEnd(';')).Append(';');
        }
        if (Inline)
        {
            sb.Append("display: inline-grid;");
        }
        else
        {
            sb.Append("display: grid;");
        }
        return sb.ToString();
    }
}