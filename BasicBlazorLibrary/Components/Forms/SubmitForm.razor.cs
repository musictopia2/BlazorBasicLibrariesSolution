using BasicBlazorLibrary.Components.InputNavigations;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
namespace BasicBlazorLibrary.Components.Forms;
public partial class SubmitForm
{
    private readonly Func<Task> _handleSubmitDelegate; // Cache to avoid per-render allocations
    
    //trying to not use fixed
    //looks like i have to risk doing fixed being equal to true for the editcontext (so the validations work).
    //if this causes issues, then i can have a parameter to determine whether its fixed or not.  default would be fixed.
    //so for cases where its not an issue, then would work fine.

    internal EditContext? _editContext;
    private bool _hasSetEditContextExplicitly;
    public bool Validate => _editContext!.Validate();
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
    
    
    [CascadingParameter]
    public InputTabOrderNavigationContainer? TabContainer { get; set; }
    public async Task FocusNextAsync()
    {
        await TabContainer!.FocusNextAsync(); //so can use this one if you wish.
    }
    /// <summary>
    /// Supplies the edit context explicitly. If using this parameter, do not
    /// also supply <see cref="Model"/>, since the model value will be taken
    /// from the <see cref="EditContext.Model"/> property.
    /// </summary>
    [Parameter]
#pragma warning disable BL0007 // Component parameters should be auto properties
//cannot be auto properties because the off the shelf editform does not do autoproperties.  plus needs to specify the variable which helps later.
    public EditContext? EditContext
#pragma warning restore BL0007 // Component parameters should be auto properties
    {
        get => _editContext;
        set
        {
            _editContext = value;
            _hasSetEditContextExplicitly = value != null;
        }
    }
    /// <summary>
    /// Specifies the top-level model object for the form. An edit context will
    /// be constructed for this model. If using this parameter, do not also supply
    /// a value for <see cref="EditContext"/>.
    /// </summary>
    [Parameter]
    public object? Model { get; set; }
    /// <summary>
    /// Specifies the content to be rendered inside this <see cref="SubmitForm"/>.
    /// </summary>
    [Parameter] public RenderFragment<EditContext>? ChildContent { get; set; }
    /// <summary>
    /// A callback that will be invoked when the form is submitted.
    ///
    /// If using this parameter, you are responsible for triggering any validation
    /// manually, e.g., by calling <see cref="EditContext.Validate"/>.
    /// </summary>
    [Parameter] public EventCallback<EditContext> OnSubmit { get; set; }

    /// <summary>
    /// A callback that will be invoked when the form is submitted and the
    /// <see cref="EditContext"/> is determined to be valid.
    /// </summary>
    [Parameter] public EventCallback<EditContext> OnValidSubmit { get; set; }

    /// <summary>
    /// A callback that will be invoked when the form is submitted and the
    /// <see cref="EditContext"/> is determined to be invalid.
    /// </summary>
    [Parameter] public EventCallback<EditContext> OnInvalidSubmit { get; set; }

    protected override void OnParametersSet()
    {
        if (_hasSetEditContextExplicitly && Model != null)
        {
            throw new InvalidOperationException($"{nameof(SubmitForm)} requires a {nameof(Model)} " +
                $"parameter, or an {nameof(EditContext)} parameter, but not both.");
        }
        else if (!_hasSetEditContextExplicitly && Model == null)
        {
            throw new InvalidOperationException($"{nameof(SubmitForm)} requires either a {nameof(Model)} " +
                $"parameter, or an {nameof(EditContext)} parameter, please provide one of these.");
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

        if (Model != null && Model != _editContext?.Model)
        {
            _editContext = new EditContext(Model!);
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
            await OnSubmit.InvokeAsync(_editContext);
        }
        else
        {
            await Task.Delay(20);
            if (_usedHotkey)
            {
                await TabContainer!.FocusNextAsync();
            }
            // Otherwise, the system implicitly runs validation on form submission
            var isValid = _editContext!.Validate(); // This will likely become ValidateAsync later

            if (isValid && OnValidSubmit.HasDelegate)
            {
                await OnValidSubmit.InvokeAsync(_editContext);
            }

            if (!isValid && OnInvalidSubmit.HasDelegate)
            {
                await OnInvalidSubmit.InvokeAsync(_editContext);
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