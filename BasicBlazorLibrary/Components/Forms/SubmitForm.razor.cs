using BasicBlazorLibrary.Components.InputNavigations;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
namespace BasicBlazorLibrary.Components.Forms;
public partial class SubmitForm
{
    private readonly Func<Task> _handleSubmitDelegate; // Cache to avoid per-render allocations
    internal EditContext? _fixedEditContext;
    public bool Validate => _fixedEditContext!.Validate();
    public SubmitForm()
    {
        _handleSubmitDelegate = HandleSubmitAsync;
    }
    /// <summary>
    /// if set to true, will set the height to 100% so can be compatible with the simple leftovers.
    /// </summary>
    [Parameter]
    public bool FullHeight { get; set; }
    [Parameter]
    public string SubmitKey { get; set; } = "";
    [Parameter]
    public EventCallback OnSubmit { get; set; }
    [Parameter]
    public EventCallback OnValidSubmit { get; set; }
    [Parameter]
    public EventCallback OnInvalidSubmit { get; set; }
    [Parameter]
    public RenderFragment<EditContext>? ChildContent { get; set; }
    [CascadingParameter]
    public InputTabOrderNavigationContainer? TabContainer { get; set; }
    public async Task FocusNextAsync()
    {
        await TabContainer!.FocusNextAsync(); //so can use this one if you wish.
    }
    [Parameter]
    public object? Model { get; set; }
    [Parameter] public EditContext? EditContext { get; set; }
    protected override void OnParametersSet()
    {
        if ((EditContext == null) == (Model == null))
        {
            throw new InvalidOperationException($"{nameof(EditForm)} requires a {nameof(Model)} " +
                $"parameter, or an {nameof(EditContext)} parameter, but not both.");
        }
        // If you're using OnSubmit, it becomes your responsibility to trigger validation manually
        // (e.g., so you can display a "pending" state in the UI). In that case you don't want the
        // system to trigger a second validation implicitly, so don't combine it with the simplified
        // OnValidSubmit/OnInvalidSubmit handlers.
        if (OnSubmit.HasDelegate && (OnValidSubmit.HasDelegate || OnInvalidSubmit.HasDelegate))
        {
            throw new InvalidOperationException($"When supplying an {nameof(OnSubmit)} parameter to " +
                $"{nameof(EditForm)}, do not also supply {nameof(OnValidSubmit)} or {nameof(OnInvalidSubmit)}.");
        }
        // Update _fixedEditContext if we don't have one yet, or if they are supplying a
        // potentially new EditContext, or if they are supplying a different Model
        if (_fixedEditContext == null || EditContext != null || Model != _fixedEditContext.Model)
        {
            _fixedEditContext = EditContext ?? new EditContext(Model!);
            if (TabContainer!.FocusFirst)
            {
                _needsFirstFocus = true;
            }
        }
    }
    private bool _needsFirstFocus;
    private async Task KeyDown(KeyboardEventArgs args)
    {
        if (SubmitKey == "")
        {
            return;
        }
        if (args.Key == SubmitKey)
        {
            _usedHotkey = true;
            await _handleSubmitDelegate();
            return;
        }
    }
    private bool _usedHotkey;
    protected override void OnInitialized()
    {
        _usedHotkey = false;
        base.OnInitialized();
    }
    public async Task HandleSubmitAsync()
    {
        if (OnSubmit.HasDelegate)
        {
            // When using OnSubmit, the developer takes control of the validation lifecycle
            await OnSubmit.InvokeAsync(null!);
        }
        else
        {
            await Task.Delay(20);
            if (_usedHotkey)
            {
                await TabContainer!.FocusNextAsync();
            }
            // Otherwise, the system implicitly runs validation on form submission
            var isValid = _fixedEditContext!.Validate(); // This will likely become ValidateAsync later

            if (isValid && OnValidSubmit.HasDelegate)
            {
                await OnValidSubmit.InvokeAsync(null!);
            }

            if (!isValid && OnInvalidSubmit.HasDelegate)
            {
                await OnInvalidSubmit.InvokeAsync(null!);
            }
        }
    }
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (_needsFirstFocus)
        {
            //at first, use the tab containers focusfirst.
            await TabContainer!.FocusFirstAsync();
            _needsFirstFocus = false;
        }
    }
}