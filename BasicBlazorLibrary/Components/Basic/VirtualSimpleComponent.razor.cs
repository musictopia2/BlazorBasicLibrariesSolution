namespace BasicBlazorLibrary.Components.Basic;
public struct VirtualModel<TItem> //go ahead and keep with this.
{
    public TItem Element { get; set; }
    public int Index { get; set; }
}
public partial class VirtualSimpleComponent<TItem> : IAsyncDisposable
{
    [Parameter]
    public RenderFragment<VirtualModel<TItem>>? ChildContent { get; set; }
    [Parameter]
    public BasicList<TItem> Items { get; set; } = new();
    [Parameter]
    public string ContainerHeight { get; set; } = "50vh";
    [Parameter]
    public string LineHeight { get; set; } = "1.5rem";
    [Parameter]
    public string ContainerWidth { get; set; } = "100vw";
    [Parameter]
    public bool HasSolidBlackBorders { get; set; } = false;
    [Parameter]
    public string BackgroundColor { get; set; } = cc.White.ToWebColor();
    private ScrollListenerClass? _listen;
    private AutoScrollClass? _autoScroll;
    private ElementReference? _mainScroll;
    private ElementReference? _sampleElement;
    private ElementReference? _sampleContainer;
    private int _currentScrollValue;
    private int _clientHeight;
    private int _containerHeight;
    private float _scrollAmount;
    private bool _needsToScroll;
    private int _itemHeight;
    protected override void OnInitialized()
    {
        _listen = new ScrollListenerClass(JS!);
        _autoScroll = new AutoScrollClass(JS!);
        _listen.Scrolled += Listen_Scroll;
        _mainScroll = null;
        _sampleElement = null;
        _sampleContainer = null;
        base.OnInitialized();
    }
    private void Listen_Scroll(int obj)
    {
        _currentScrollValue = obj;
        StateHasChanged();
    }
    private string GetBorders()
    {
        if (HasSolidBlackBorders == false)
        {
            return "";
        }
        return "border: 1px solid;";
    }
    private int GetNextItem()
    {
        int _nextUp;
        _nextUp = _itemHeight;
        int count = Items.Count;
        for (int i = 0; i < count; i++)
        {
            if (_currentScrollValue < _nextUp)
            {
                if (_currentScrollValue + _clientHeight == _containerHeight)
                {
                    return count - ElementsFit();
                }
                return i;
            }
            _nextUp += _itemHeight;
        }
        return count;
    }
    private VirtualModel<TItem> GetContent(int index)
    {
        return new VirtualModel<TItem>()
        {
            Element = Items[index],
            Index = index
        };
    }
    private string GetUnitMeasure()
    {
        BasicList<string> units = new() { "rem", "em", "px", "vw", "vh", "vmin", "vmax", "%" };
        foreach (var unit in units)
        {
            if (LineHeight.EndsWith(unit))
            {
                return unit;
            }
        }
        throw new CustomBasicException("No unit measure found");
    }
    private string GetContainerSize()
    {
        string extraText;
        string leftovers;
        float firsts;
        extraText = GetUnitMeasure();
        leftovers = LineHeight.Replace(extraText, "");
        firsts = float.Parse(leftovers);
        var seconds = firsts * Items.Count;
        return $"{seconds}{extraText}";
    }
    private int ElementsFit()
    {
        int partialheight = _clientHeight;
        float singlepixel = _itemHeight;
        int output = partialheight / (int)singlepixel;
        if (output > Items.Count)
        {
            output = Items.Count;
        }
        else if (output + 5 > Items.Count)
        {
            output = Items.Count;
        }
        else
        {
            output += 5;
        }
        return output;
    }
    private string GetPosition(int element)
    {
        int item = element * _itemHeight;
        return $"{item}px;";
    }
    public void ScrollToSpecificElement(int whichOne)
    {
        _needsToScroll = true;
        _scrollAmount = _itemHeight * whichOne;
    }
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        _clientHeight = await JS!.GetContainerHeight(_mainScroll);
        _itemHeight = await JS!.GetContainerHeight(_sampleElement);
        _containerHeight = await JS!.GetContainerHeight(_sampleContainer);
        if (firstRender)
        {
            await _listen!.InitAsync(_mainScroll);
            StateHasChanged();
            return;
        }
        if (_needsToScroll)
        {
            await _autoScroll!.SetScrollPosition(_mainScroll, _scrollAmount); //i think
            _needsToScroll = false;
        }
    }
    public ValueTask DisposeAsync()
    {

        _listen!.Scrolled -= Listen_Scroll;
        return ValueTask.CompletedTask;
    }
}