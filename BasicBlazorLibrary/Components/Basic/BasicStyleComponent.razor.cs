namespace BasicBlazorLibrary.Components.Basic;
public partial class BasicStyleComponent
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    [Parameter]
    public BasicList<string> ExtraCssFiles { get; set; } = new();

    //[Parameter]
    //public RenderFragment? HeadContent { get; set; }
    public const string Btn = "btn"; //this will help with intellisense for different class names.
    public const string NoSelect = "usernoselect";
    public const string Bold = "bold";
    public const string Table = "table";
    public const string TableStriped = "table-striped";
    public const string TheadDark = "thead-dark";
    //this will be all the stuff from either bootstrap i really like or would be any generic classes i really want.
    //if i do generic classes, then i have to know what to name it so its easy to use.
}