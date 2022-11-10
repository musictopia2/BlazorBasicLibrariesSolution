using BasicBlazorLibrary.Components.DataEntryHelpers;
using BasicBlazorLibrary.Components.InputNavigations;
namespace BasicBlazorLibrary.Components.BaseClasses;
public abstract class TextComponentBase<TValue> : JavascriptComponentBase, IFocusInput, IDisposable
{
    private bool _disposedValue;

    [CascadingParameter]
    public InputTabOrderNavigationContainer? TabContainer { get; set; }
    [Parameter]
    public bool FocusFirst { get; set; }
    [Parameter]
    public string Title { get; set; } = "";
    [Parameter]
    public string HeaderFontSize { get; set; } = "";
    protected string GetHeaderFontSize => HeaderFontSize == "" ? "1em" : HeaderFontSize;
    [Parameter]
    public string Class { get; set; } = "";
    [Parameter]
    public string Style { get; set; } = "";
    [Parameter]
    public bool SpellCheck { get; set; }
    /// <summary>
    /// Gets or sets the value of the input. This should be used with two-way binding.
    /// </summary>
    /// <example>
    /// @bind-Value="model.PropertyName"
    /// </example>
    [Parameter] public TValue? Value { get; set; }
    /// <summary>
    /// Gets or sets a callback that updates the bound value.
    /// </summary>
    [Parameter] public EventCallback<TValue> ValueChanged { get; set; }
    [Parameter]
    public EventCallback OnEnterPressed { get; set; }
    public ElementReference? InputElement { get; set; }
    /// <summary>
    /// Gets or sets the current value of the input.
    /// </summary>
    protected TValue? CurrentValue
    {
        get => Value;
        set
        {
            var hasChanged = !EqualityComparer<TValue>.Default.Equals(value, Value);
            if (hasChanged)
            {
                Value = value;
                _ = ValueChanged.InvokeAsync(value);
            }
        }
    }
    protected KeystrokeClass? KeyStrokeHelper;
    protected override void OnInitialized()
    {
        if (TabContainer is not null)
        {
            KeyStrokeHelper = new(TabContainer.JS!);
            KeyStrokeHelper.AddShiftTab(ProcessShiftTab);
            KeyStrokeHelper.AddAction(ConsoleKey.Tab, ProcessEnter);
            KeyStrokeHelper.AddAction(ConsoleKey.Enter, ProcessEnter);
            TabContainer.AddFocusItem(this);
        }
        else if (OnEnterPressed.HasDelegate)
        {
            KeyStrokeHelper = new(JS!);
            KeyStrokeHelper.AddAction(ConsoleKey.Enter, ProcessEnter);
        }
        base.OnInitialized();
    }
    public int TabIndex { get; set; } = -1;
    public async Task FocusAsync()
    {
        await Task.Delay(50);
        if (TabContainer is not null)
        {
            await TabContainer.FocusAndSelectAsync(InputElement);
        }
        else
        {
            await InputElement!.Value.FocusAsync();
        }
    }
    protected async void ProcessEnter()
    {
        await LoseFocusAsync();
        if (OnEnterPressed.HasDelegate)
        {
            await OnEnterPressed.InvokeAsync();
            return;
        }
        if (TabContainer is null)
        {
            return;
        }
        await TabContainer.FocusNextAsync();
    }

    private async void ProcessShiftTab()
    {
        await LoseFocusAsync();
        if (TabContainer is null)
        {
            return;
        }
        await TabContainer.FocusPreviousAsync();
    }
    protected string GetStyle
    {
        get
        {
            if (Style != "")
            {
                return Style;
            }
            if (Item is not null)
            {
                return "width: 100%";
            }
            return "";
        }
    }
    [CascadingParameter]
    private DataEntryItem? Item { get; set; }
    protected bool CanHaveTitle => Item is null;
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender && FocusFirst && KeyStrokeHelper is null && InputElement is not null)
        {
            await InputElement!.Value.FocusAsync();
            return;
        }
        if (firstRender && KeyStrokeHelper is not null)
        {
            await OnFirstRenderAsync();
            await KeyStrokeHelper.InitAsync(InputElement);
            if (FocusFirst && TabContainer is not null)
            {
                await TabContainer.FocusSpecificInputAsync(this); //so it can set properly.
            }
            else if (FocusFirst)
            {
                await InputElement!.Value.FocusAsync();
            }
        }
        else if (firstRender == false && KeyStrokeHelper is not null)
        {
            await OnAfterFirstRenderAsync();
        }
    }
    protected virtual Task OnAfterFirstRenderAsync()
    {
        return Task.CompletedTask;
    }
    protected virtual Task OnFirstRenderAsync()
    {
        return Task.CompletedTask;
    }
    public virtual Task LoseFocusAsync()
    {
        //now we have to do something with losefocus
        //because we can have a date component that requires no validation stuff.
        return Task.CompletedTask;
    }
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                if (TabContainer is not null)
                {
                    TabContainer.RemoveFocusItem(this);
                }
            }
            _disposedValue = true;
        }
    }
    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}