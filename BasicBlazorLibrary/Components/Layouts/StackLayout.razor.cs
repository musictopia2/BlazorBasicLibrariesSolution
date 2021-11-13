using CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.Misc;
using System.Text;
namespace BasicBlazorLibrary.Components.Layouts;
public partial class StackLayout
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
    [Parameter]
    public EnumOrientation Orientation { get; set; } = EnumOrientation.Horizontal; //attempt to do horizontal.  most of the time, that is most common.
    public ElementReference? MainElement { get; set; }
    [Parameter]
    public EnumOverflowCategory Overflow { get; set; } = EnumOverflowCategory.None;
    [Parameter]
    public string Width { get; set; } = "100%";
    [Parameter]
    public string Height { get; set; } = "100%";
    [Parameter]
    public string FontSize { get; set; } = "";
    [Parameter]
    public bool FullScreen { get; set; }
    [Parameter]
    public string Style { get; set; } = "";
    /// <summary>
    /// If set to true then the layout of the grid will be "inline" instead of stretching to fill the container.
    /// </summary>
    /// <value>Default is false</value>
    [Parameter]
    public bool Inline { get; set; }
    [Parameter]
    public string ItemSpacing { get; set; } = "3px";
    public void Refresh()
    {
        StateHasChanged();
    }
    private readonly BasicList<StackItem> _stackList = new();
    public int AddChild(StackItem child)
    {
        _stackList.Add(child);
        int output = _stackList.Count;
        return output;
    }
    private string GetOverflow => Overflow switch
    {
        EnumOverflowCategory.Auto => "",
        EnumOverflowCategory.Clip => "hidden",
        EnumOverflowCategory.Scrollable => "auto",
        _ => "hidden"
    };
    private string GetStyle()
    {
        StringBuilder sb = new();
        if (FullScreen)
        {
            sb.Append("height: 200px;");
            sb.Append("width: 100%;");
        }
        else
        {
            if (Height != "" && Orientation == EnumOrientation.Vertical)
            {
                sb.Append($"height: {Height};");
            }
            if (Width != "" && Orientation == EnumOrientation.Horizontal)
            {
                sb.Append($"width: {Width};");
            }
        }
        if (Inline)
        {
            sb.Append("display: inline-grid;");
        }
        else
        {
            sb.Append("display: grid;");
        }
        StrCat cats = new();
        _stackList.ForEach(xxx =>
        {
            cats.AddToString(xxx.Length, " ");
        });
        if (Orientation == EnumOrientation.Horizontal && ItemSpacing != "")
        {
            sb.Append($"grid-column-gap: {ItemSpacing};");
        }
        if (Orientation == EnumOrientation.Vertical && ItemSpacing != "")
        {
            sb.Append($"grid-row-gap: {ItemSpacing};");
        }
        if (Orientation == EnumOrientation.Horizontal)
        {
            sb.Append($"grid-template-columns: {cats.GetInfo()};");
        }
        else
        {
            sb.Append($"grid-template-rows: {cats.GetInfo()};");
        }
        if (Overflow != EnumOverflowCategory.None)
        {
            sb.Append($"overflow: {GetOverflow};");
        }
        if (FontSize != "")
        {
            sb.Append($"font-size: {FontSize}");
        }
        if (Style != "")
        {
            sb.Append(Style);
        }
        string output = sb.ToString();
        return output;
    }
}