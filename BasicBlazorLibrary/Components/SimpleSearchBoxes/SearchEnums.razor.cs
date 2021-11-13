using BasicBlazorLibrary.Components.AutoCompleteHelpers;
using System.Globalization;
namespace BasicBlazorLibrary.Components.SimpleSearchBoxes;
public partial class SearchEnums<TValue>
    where TValue : Enum
{
    [Parameter]
    public TValue? Value { get; set; }
    [Parameter]
    public EventCallback<TValue> ValueChanged { get; set; }
    [Parameter]
    public EventCallback SearchEnterPressed { get; set; }
    [Parameter]
    public AutoCompleteStyleModel Style { get; set; } = new AutoCompleteStyleModel(); //i like this idea.
    [Parameter]
    public int TabIndex { get; set; } = -1;
    [Parameter]
    public string Placeholder { get; set; } = "";
    [Parameter]
    public bool RequiredTab { get; set; } = false;
    [Parameter]
    public bool RemoveFirstValue { get; set; }
    public ElementReference? TextReference => _search!.GetTextBox;
    private SearchStringList? _search;

    private readonly BasicList<string> _list = new();
    private TValue FirstValue { get; set; } = default!;
    protected override void OnInitialized()
    {
        _search = null;
        BasicList<TValue> list;
        if (ItemList.Count > 0)
        {
            list = ItemList.ToBasicList();
        }
        else
        {
            list = new();
            var firsts = Enum.GetValues(typeof(TValue));
            foreach (var item in firsts)
            {
                list.Add((TValue)item);
            }
        }
        int x = 0;
        foreach (var item in list)
        {
            if (x == 0 && RemoveFirstValue)
            {
                BindConverter.TryConvertTo<TValue>(item.ToString(), CultureInfo.CurrentCulture, out var ff);
                FirstValue = ff!;
                continue;
            }
            if (item.ToString() != "None")
            {
                _list.Add(item.ToString()!);
            }
            else
            {
                BindConverter.TryConvertTo<TValue>(item.ToString(), CultureInfo.CurrentCulture, out var ff);
                FirstValue = ff!;
            }
            x++;
        }
        _list.Sort();
    }
    [Parameter]
    public BasicList<TValue> ItemList { get; set; } = new();
    private string _textDisplay = "";
    protected override void OnParametersSet()
    {
        if (Value!.Equals(FirstValue))
        {
            _textDisplay = "";
        }
        else
        {
            _textDisplay = Value.ToString();
        }
        base.OnParametersSet();
    }
    private void TextChanged(string value)
    {
        var success = BindConverter.TryConvertTo<TValue>(value, CultureInfo.CurrentCulture, out var parsedValue);
        if (success == false)
        {
            _textDisplay = "";
            return;
        }
        if (parsedValue!.Equals(FirstValue))
        {
            _textDisplay = "";
            return;
        }
        ValueChanged.InvokeAsync(parsedValue);
    }
}