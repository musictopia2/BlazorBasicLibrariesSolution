namespace BasicBlazorLibrary.Components.AutoCompleteHelpers;
public class AutoCompleteService
{
    private readonly AutoScrollClass _scrollHelper;
    private readonly KeystrokeClass _keystroke;
    public event Action? ArrowUp;
    public event Action? ArrowDown;
    public event Action? BackspacePressed;
    public int ElementHighlighted { get; private set; } = -1;
    public int ElementScrollTo { get; private set; } = -1;
    public bool NeedsToScroll { get; private set; }
    private int Previoushighlight { get; set; } = -1;
    private int TotalElements { get; set; }
    public AutoCompleteService(IJSRuntime js)
    {
        _scrollHelper = new AutoScrollClass(js);
        _keystroke = new KeystrokeClass(js);
        _keystroke.AddAction(ConsoleKey.Backspace, () =>
        {
            ElementHighlighted = -1;
            NeedsToScroll = false;
            ElementScrollTo = -1;
            BackspacePressed?.Invoke();
        });
        _keystroke.AddArrowUpAction(() => ArrowUp?.Invoke());
        _keystroke.AddArrowDownAction(() => ArrowDown?.Invoke());
    }
    public void DoHighlight(int value, bool alsoscroll)
    {
        NeedsToScroll = alsoscroll;
        if (alsoscroll)
        {
            ElementScrollTo = value;
        }
        ElementHighlighted = value;
        Previoushighlight = value;
    }
    public void Unhighlight(bool alsoscroll)
    {
        NeedsToScroll = alsoscroll;
        ElementHighlighted = -1;
        Previoushighlight = -1;
        ElementScrollTo = -1;
    }
    public void MoveDown()
    {
        NeedsToScroll = false;
        if (Previoushighlight > -1 && ElementHighlighted == -1)
        {
            NeedsToScroll = true;
            ElementHighlighted = Previoushighlight;
            ElementScrollTo = ElementHighlighted;
            return;
        }
        if (ElementHighlighted + 1 == TotalElements)
        {
            return;
        }
        ElementHighlighted++;
        Previoushighlight = ElementHighlighted;
        ElementScrollTo = ElementHighlighted;
        NeedsToScroll = true;
    }
    public void MoveUp()
    {
        NeedsToScroll = false;
        if (Previoushighlight > -1 && ElementHighlighted == -1)
        {
            NeedsToScroll = true;
            ElementHighlighted = Previoushighlight;
            ElementScrollTo = ElementHighlighted;
            return;
        }
        if (ElementHighlighted == 0)
        {
            return;
        }
        ElementHighlighted--;
        Previoushighlight = ElementHighlighted;
        ElementScrollTo = ElementHighlighted;
        NeedsToScroll = true;
    }
    public void Update(int elements)
    {
        TotalElements = elements;
    }
    public async Task ScrollToElementAsync(ElementReference? element)
    {
        if (element == null)
        {
            return;
        }
        NeedsToScroll = false;
        await _scrollHelper.ScrollToElementAsync(element);
    }
    public async Task InitializeAsync(ElementReference? element) 
    {
        if (element == null)
        {
            return;
        }
        await _keystroke.InitAsync(element);
    }
}