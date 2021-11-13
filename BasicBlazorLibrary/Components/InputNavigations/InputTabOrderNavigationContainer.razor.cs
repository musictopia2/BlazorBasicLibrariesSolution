namespace BasicBlazorLibrary.Components.InputNavigations;
public partial class InputTabOrderNavigationContainer
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    [Parameter]
    public bool FocusFirst { get; set; }
    public bool OtherScreen { get; set; } = false; //if you are on other screen, then can ignore some things.
    private ClickInputHelperClass? _clicker;
    private FocusClass? _focusjs;
    private readonly BasicList<IFocusInput> _inputs = new();
    private int _currentTab;
    private int _max;
    private int _min;
    protected override void OnInitialized()
    {
        _clicker = new ClickInputHelperClass(JS!);
        _focusjs = new FocusClass(JS!);
        _clicker.InputClicked = (tabindex) =>
        {
            if (tabindex != _currentTab)
            {
                LoseFocus();
            }
            _currentTab = tabindex;
        };

        _clicker.OtherClicked = LoseFocus;
        base.OnInitialized();
    }
    private async void LoseFocus()
    {
        if (OtherScreen == true)
        {
            return;
        }
        if (_currentTab <= 0)
        {
            return;
        }
        var input = _inputs.FirstOrDefault(thisitem => thisitem.TabIndex == _currentTab);
        if (input != null)
        {
            await input.LoseFocusAsync();
        }
        _currentTab = 0;
    }
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await _clicker!.InitAsync();
            if (FocusFirst)
            {
                await FocusFirstAsync();
            }
        }
    }
    public void RemoveFocusItem(IFocusInput input)
    {
        _inputs.RemoveSpecificItem(input);
        if (_inputs.Count == 0)
        {
            _max = 0;
            _min = 0;
        }
        else
        {
            _max = _inputs.Max(thisitem => thisitem.TabIndex);
            _min = _inputs.Min(thisitem => thisitem.TabIndex);
        }
    }
    public void ClearAllItems()
    {
        _inputs.Clear();
        _max = 0;
        _min = 0;
    }
    public void AddFocusItem(IFocusInput input)
    {
        if (input.TabIndex > 0)
        {
            if (_inputs.Any(thisitem => thisitem.TabIndex == input.TabIndex))
            {
                throw new CustomBasicException("Duplicate tab indexes.  May eventually require rethinking");
            }
            _inputs.Add(input);
            AnalyzeMinMax(input.TabIndex);
            return;
        }
        input.TabIndex = _max + 1;
        AnalyzeMinMax(input.TabIndex);
        _inputs.Add(input);
    }
    public void ShowTabItems()
    {
        foreach (var item in _inputs)
        {
            Console.WriteLine(item.TabIndex);
        }
    }
    private void AnalyzeMinMax(int tabindex)
    {
        if (tabindex > _max)
        {
            _max = tabindex;
        }
        if (tabindex < _min)
        {
            _min = tabindex;
        }
    }
    private async Task StartFocusAsync(Func<Task> action)
    {
        if (_inputs.Count == 0)
        {
            return;
        }
        if (_inputs.Count == 1)
        {
            _currentTab = _inputs.Single().TabIndex;
            await FocusCurrentAsync();
            return;
        }
        await action.Invoke();
    }
    public async Task FocusFirstAsync()
    {
        await StartFocusAsync(async () =>
        {
            _currentTab = _inputs.Min(thisitem => thisitem.TabIndex);
            await FocusCurrentAsync();
        });
    }
    public async Task FocusAndSelectAsync(ElementReference? element)
    {
        if (element == null)
        {
            return;
        }
        await _focusjs!.FocusAsync(element);
    }
    public void ResetFocus(IFocusInput input)
    {
        _currentTab = input.TabIndex;
    }
    public async Task FocusSpecificInputAsync(IFocusInput input)
    {
        if (input.TabIndex == 0)
        {
            throw new CustomBasicException("TabIndex cannot be 0.  Find out what happened");
        }
        await input.FocusAsync();
        _currentTab = input.TabIndex;
    }
    public async Task FocusLastAsync()
    {
        await StartFocusAsync(async () =>
        {
            _currentTab = _inputs.Max(thisitem => thisitem.TabIndex);
            await FocusCurrentAsync();
        });
    }
    public async Task FocusCurrentAsync()
    {
        await _inputs.First(thisitem => thisitem.TabIndex == _currentTab).FocusAsync();
    }
    public async Task FocusNextAsync()
    {
        await StartFocusAsync(async () =>
        {
            int next = _currentTab + 1;
            do
            {
                if (_inputs.Exists(thisitem => thisitem.TabIndex == next) == true)
                {
                    break;
                }

                next++;
                if (next > _max)
                {
                    return;
                }

            } while (true);
            _currentTab = next;
            await FocusCurrentAsync();
        });
    }
    public async Task FocusPreviousAsync()
    {
        await StartFocusAsync(async () =>
        {
            int previous = _currentTab - 1;
            do
            {
                if (_inputs.Exists(thisitem => thisitem.TabIndex == previous) == true)
                {
                    break;
                }
                previous--;
                if (previous < _min)
                {
                    return;
                }

            } while (true);
            _currentTab = previous;
            await FocusCurrentAsync();
        });
    }
}