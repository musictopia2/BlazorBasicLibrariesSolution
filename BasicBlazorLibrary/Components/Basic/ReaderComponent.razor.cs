namespace BasicBlazorLibrary.Components.Basic;
public partial class ReaderComponent<T> : IAsyncDisposable
{
    private record ScrollState(BasicList<T>? List, int ElementScrollTo);
    /// <summary>
    /// this is needed because in order for the scrollbars to work properly should have the width and height of the area.
    /// </summary>
    [Parameter]
    public string Width { get; set; } = "80vw";
    /// <summary>
    /// this is needed because if it uses the browser scroll bars, then does not scroll exactly to the proper area.
    /// </summary>
    [Parameter]
    public string Height { get; set; } = "80vh";
    //good news is even if i have multiple chapters, still works because i can send anything i want here.
    [Parameter]
    public BasicList<T>? RenderList { get; set; } //this needs to be here though.
    [Parameter]
    public RenderFragment<T>? ChildContent { get; set; }
    [Parameter]
    public ReaderModel? DataContext { get; set; } //if you change values of the readermodel, then no problem because does not run parametershaschanged.
    [Parameter]
    public EventCallback TopReached { get; set; }
    [Parameter]
    public EventCallback BottomReached { get; set; }
    [Parameter]
    public bool CanFocusFirst { get; set; } = true;
    [Parameter]
    public bool Enabled { get; set; } = true; //sometimes won't even be enabled.
    private bool NeedsHighlighting => DataContext!.HighlightColor != cc.Transparent.ToWebColor();
    private KeystrokeClass? _keystroke;
    private AutoScrollClass? _autoScroll;
    private string GetColorStyle(int id)
    {
        if (NeedsHighlighting == false)
        {
            return "";
        }
        if (id != DataContext!.ElementHighlighted)
        {
            return ""; //because its not the correct one.
        }
        return $"background-color: {DataContext.HighlightColor};";
    }
    private string GetClass => DataContext!.ScrollVisible ? "customscroll" : "noscroll";
    private ElementReference? _reference;
    private ElementReference? _main;
    private bool _needsToScroll = true;
    private void ElementClicked(int x)
    {
        if (NeedsHighlighting == false || Enabled == false)
        {
            return;
        }
        DataContext!.ElementHighlighted = x;
    }
    protected override void OnParametersSet()
    {
        ScrollState current = GetRecord;
        if (current.Equals(_previousRecord))
        {
            return;
        }
        _previousRecord = current;
        if (WasSame)
        {
            ResetValues();
            return;
        }
        _needsToScroll = true;
        PrepScrolling();
        base.OnParametersSet();
    }
    protected override void OnInitialized()
    {
        _reference = null;
        _previousRecord = GetRecord;
        _main = null;
        _keystroke = new KeystrokeClass(JS!);
        if (Enabled)
        {
            _keystroke.AddArrowUpAction(() => ArrowUp(false));
            _keystroke.AddArrowDownAction(() => ArrowDown(false));
        }
        _autoScroll = new AutoScrollClass(JS!);
        PrepScrolling();
    }
    private void PrepScrolling()
    {
        if (DataContext!.ElementScrollTo > -1)
        {
            DataContext.ElementHighlighted = DataContext.ElementScrollTo;
        }
        ResetValues();
    }
    private ScrollState GetRecord => new(RenderList, GetScrollTo);
    private int GetScrollTo
    {
        get
        {
            if (DataContext != null)
            {
                return DataContext.ElementScrollTo;
            }
            return 0;
        }
    }
    private ScrollState? _previousRecord;
    private BasicList<T>? _previousList;
    private int _previousHighlighted;
    private int _previousScrolled;
    private void ResetValues()
    {
        _previousList = null;
        _previousHighlighted = -1;
        _previousScrolled = -1;
    }
    private bool WasSame
    {
        get
        {
            if (_previousList == null)
            {
                return false;
            }
            if (_previousHighlighted == -1 || _previousScrolled == -1)
            {
                return false;
            }
            if (_previousList.Count != RenderList!.Count)
            {
                return false;
            }
            if (_previousHighlighted != DataContext!.ElementHighlighted || _previousScrolled != DataContext!.ElementScrollTo)
            {
                return false;
            }
            return true;
        }
    }
    private void SetValues()
    {
        _previousList = RenderList!.ToBasicList();
        _previousHighlighted = DataContext!.ElementHighlighted;
        _previousScrolled = DataContext!.ElementScrollTo;
    }
    public void ArrowUp()
    {
        ArrowUp(true);
    }
    private void ArrowUp(bool manuel)
    {
        if (DataContext!.ElementHighlighted == 0)
        {
            SetValues();
            TopReached.InvokeAsync();
            return;
        }
        DataContext.ElementHighlighted--;
        DataContext.ElementScrollTo = DataContext.ElementHighlighted;
        _needsToScroll = true;
        if (manuel == false)
        {
            StateHasChanged();
        }
    }
    public void ArrowDown()
    {
        ArrowDown(true);
    }
    private void ArrowDown(bool manuel)
    {
        if (DataContext!.ElementHighlighted + 1 == RenderList!.Count)
        {
            SetValues();
            BottomReached.InvokeAsync();
            return;
        }
        DataContext!.ElementHighlighted++;
        DataContext.ElementScrollTo = DataContext.ElementHighlighted;
        _needsToScroll = true;
        if (manuel == false)
        {
            StateHasChanged();
        }
    }
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (_needsFocus && _main != null)
        {
            await _main.Value.FocusAsync();
            _needsFocus = false;
        }
        else if (_needsFocus)
        {
            throw new CustomBasicException("Main element was not created even after render but needed focus.  Rethink");
        }
        if (_needsToScroll == false || _keystroke == null)
        {
            return;
        }
        await _keystroke.InitAsync(_main); //maybe has to be this way.  this time, can do everytime.
        if (firstRender)
        {
            if (CanFocusFirst && _main != null)
            {
                await _main!.Value.FocusAsync();
            }
        }
        if (_reference == null)
        {
            return; //try this way.  hopefully no never ending loop (?)
        }
        await _autoScroll!.ScrollToElementAsync(_reference);
        _reference = null; //for next time (?)
        _needsToScroll = false;
    }
    private bool _needsFocus;
    public async Task FocusAsync()
    {
        if (_main != null)
        {
            await _main.Value.FocusAsync();
            return;
        }
        _needsFocus = true;
    }
    public ValueTask DisposeAsync()
    {
        _keystroke!.RemoveAllActions();
        return ValueTask.CompletedTask;
    }
}