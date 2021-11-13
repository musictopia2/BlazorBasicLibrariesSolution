﻿using BasicBlazorLibrary.Components.DataEntryHelpers;
using BasicBlazorLibrary.Components.Forms;
using BasicBlazorLibrary.Components.InputNavigations;
using Microsoft.AspNetCore.Components.Forms;
using System.Linq.Expressions;
#nullable disable
namespace BasicBlazorLibrary.Components.Inputs;

/// <summary>
/// starting point for submitting a form via custom means using enter instead of tabs to navigate through form.
/// </summary>
/// <typeparam name="TValue"></typeparam>
public abstract class EnterBase<TValue> : ComponentBase, IFocusInput, IDisposable
{
    private readonly EventHandler<ValidationStateChangedEventArgs> _validationStateChangedHandler;
    private bool _previousParsingAttemptFailed;
    private ValidationMessageStore _parsingValidationMessages;
    private Type _nullableUnderlyingType;
    [CascadingParameter] EditContext CascadedEditContext { get; set; }
    [CascadingParameter]
    public SubmitForm Form { get; set; }
    [CascadingParameter]
    public InputTabOrderNavigationContainer TabContainer { get; set; }
    public int TabIndex { get; set; } = -1;
    [Parameter]
    public string Placeholder { get; set; } = "";
    public ElementReference? InputElement { get; set; }
    protected KeystrokeClass KeyStrokeHelper;
    private async void ProcessShiftTab()
    {
        await LoseFocusAsync();
        await TabContainer.FocusPreviousAsync();
    }
    /// <summary>
    /// this is used for cases like the date picker where its going to either submit or go to another field.
    /// which means if there is anything to do in order to get the state proper for the object can be done.
    /// </summary>
    public virtual Task LoseFocusAsync()
    {
        return Task.CompletedTask;
    }
    protected virtual bool AllowEnter => true;
    protected virtual async void ProcessEnter()
    {
        await LoseFocusAsync();
        if (WasSubmit)
        {
            await Form.HandleSubmitAsync();
            return;
        }
        await TabContainer.FocusNextAsync();
    }
    protected override void OnInitialized()
    {
        KeyStrokeHelper = new KeystrokeClass(TabContainer.JS);
        KeyStrokeHelper.AddShiftTab(ProcessShiftTab);
        KeyStrokeHelper.AddAction(ConsoleKey.Tab, ProcessEnter);
        if (AllowEnter)
        {
            KeyStrokeHelper.AddAction(ConsoleKey.Enter, ProcessEnter);
        }
        AddOtherHotKeys();
        TabContainer.AddFocusItem(this);
        base.OnInitialized();
    }
    protected virtual void AddOtherHotKeys()
    {
    }
    public async Task FocusAsync()
    {
        await TabContainer.FocusAndSelectAsync(InputElement);
    }
    [Parameter]
    public bool WasSubmit { get; set; }
    [Parameter]
    public bool FocusFirst { get; set; }
    /// <summary>
    /// Gets or sets a collection of additional attributes that will be applied to the created element.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)] public IReadOnlyDictionary<string, object> AdditionalAttributes { get; set; }
    /// <summary>
    /// Gets or sets the value of the input. This should be used with two-way binding.
    /// </summary>
    /// <example>
    /// @bind-Value="model.PropertyName"
    /// </example>
    [Parameter] public TValue Value { get; set; }
    /// <summary>
    /// Gets or sets a callback that updates the bound value.
    /// </summary>
    [Parameter] public EventCallback<TValue> ValueChanged { get; set; }
    /// <summary>
    /// Gets or sets an expression that identifies the bound value.
    /// </summary>
    [Parameter] public Expression<Func<TValue>> ValueExpression { get; set; }
    /// <summary>
    /// Gets the associated <see cref="Forms.EditContext"/>.
    /// </summary>
    protected EditContext EditContext { get; set; }
    /// <summary>
    /// Gets the <see cref="FieldIdentifier"/> for the bound value.
    /// </summary>
    protected FieldIdentifier FieldIdentifier { get; set; }
    /// <summary>
    /// Gets or sets the current value of the input.
    /// </summary>
    protected TValue CurrentValue
    {
        get => Value;
        set
        {
            var hasChanged = !EqualityComparer<TValue>.Default.Equals(value, Value);
            if (hasChanged)
            {
                Value = value;
                _ = ValueChanged.InvokeAsync(value);
                EditContext.NotifyFieldChanged(FieldIdentifier);
                AfterCurrentChanged();
            }
        }
    }
    [CascadingParameter]
    protected DataEntryItem Item { get; set; }
    protected virtual void AfterCurrentChanged() { }
    /// <summary>
    /// Gets or sets the current value of the input, represented as a string.
    /// </summary>
    protected string CurrentValueAsString
    {
        get => FormatValueAsString(CurrentValue);
        set
        {
            _parsingValidationMessages?.Clear();
            bool parsingFailed;
            if (_nullableUnderlyingType != null && string.IsNullOrEmpty(value))
            {
                // Assume if it's a nullable type, null/empty inputs should correspond to default(T)
                // Then all subclasses get nullable support almost automatically (they just have to
                // not reject Nullable<T> based on the type itself).
                parsingFailed = false;
                CurrentValue = default;
            }
            else if (TryParseValueFromString(value, out var parsedValue, out var validationErrorMessage))
            {
                parsingFailed = false;
                CurrentValue = parsedValue;
            }
            else
            {
                parsingFailed = true;
                if (_parsingValidationMessages == null)
                {
                    _parsingValidationMessages = new ValidationMessageStore(EditContext);
                }
                _parsingValidationMessages.Add(FieldIdentifier, validationErrorMessage);
                EditContext.NotifyFieldChanged(FieldIdentifier);
            }
            if (parsingFailed || _previousParsingAttemptFailed)
            {
                EditContext.NotifyValidationStateChanged();
                _previousParsingAttemptFailed = parsingFailed;
            }
        }
    }
    /// <summary>
    /// Constructs an instance of <see cref="InputBase{TValue}"/>.
    /// </summary>
    protected EnterBase()
    {
        _validationStateChangedHandler = (sender, eventArgs) => StateHasChanged();
    }
    /// <summary>
    /// Formats the value as a string. Derived classes can override this to determine the formating used for <see cref="CurrentValueAsString"/>.
    /// </summary>
    /// <param name="value">The value to format.</param>
    /// <returns>A string representation of the value.</returns>
    protected virtual string FormatValueAsString(TValue value)
        => value?.ToString();
    /// <summary>
    /// Parses a string to create an instance of <typeparamref name="TValue"/>. Derived classes can override this to change how
    /// <see cref="CurrentValueAsString"/> interprets incoming values.
    /// </summary>
    /// <param name="value">The string value to be parsed.</param>
    /// <param name="result">An instance of <typeparamref name="TValue"/>.</param>
    /// <param name="validationErrorMessage">If the value could not be parsed, provides a validation error message.</param>
    /// <returns>True if the value could be parsed; otherwise false.</returns>
    protected virtual bool TryParseValueFromString(string value, out TValue result, out string validationErrorMessage)
    {
        validationErrorMessage = "";
        object temps = value;
        result = (TValue)temps; //that way overrided ones don't have to do anything if they don't want to.
                                //date pickers and combos do their own thing for this.
        return true;
    }
    /// <summary>
    /// Gets a string that indicates the status of the field being edited. This will include
    /// some combination of "modified", "valid", or "invalid", depending on the status of the field.
    /// </summary>
    private string FieldClass
        => EditContext.FieldCssClass(FieldIdentifier);
    /// <summary>
    /// Gets a CSS class string that combines the <c>class</c> attribute and <see cref="FieldClass"/>
    /// properties. Derived components should typically use this value for the primary HTML element's
    /// 'class' attribute.
    /// </summary>
    protected string CssClass
    {
        get
        {
            if (AdditionalAttributes != null &&
                AdditionalAttributes.TryGetValue("class", out var @class) &&
                !string.IsNullOrEmpty(Convert.ToString(@class)))
            {
                return $"{@class} {FieldClass}";
            }

            return FieldClass; // Never null or empty
        }
    }
    /// <inheritdoc />
    public override Task SetParametersAsync(ParameterView parameters)
    {
        parameters.SetParameterProperties(this);
        if (EditContext == null)
        {
            // This is the first run
            // Could put this logic in OnInit, but its nice to avoid forcing people who override OnInit to call base.OnInit()
            if (CascadedEditContext == null)
            {
                throw new InvalidOperationException($"{GetType()} requires a cascading parameter " +
                    $"of type {nameof(EditContext)}. For example, you can use {GetType().FullName} inside " +
                    $"an {nameof(EditForm)}.");
            }

            if (ValueExpression == null)
            {
                throw new InvalidOperationException($"{GetType()} requires a value for the 'ValueExpression' " +
                    $"parameter. Normally this is provided automatically when using 'bind-Value'.");
            }
        }
        else if (CascadedEditContext != EditContext)
        {
            EditContext.OnValidationStateChanged -= _validationStateChangedHandler;
        }
        EditContext = CascadedEditContext;
        FieldIdentifier = FieldIdentifier.Create(ValueExpression);
        _nullableUnderlyingType = Nullable.GetUnderlyingType(typeof(TValue));
        EditContext.OnValidationStateChanged += _validationStateChangedHandler;
        return base.SetParametersAsync(ParameterView.Empty);
    }
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender && KeyStrokeHelper is not null)
        {
            await OnFirstRenderAsync();
            await KeyStrokeHelper.InitAsync(InputElement);
            if (FocusFirst)
            {
                await TabContainer.FocusSpecificInputAsync(this); //so it can set properly.
            }
        }
    }
    protected virtual Task OnFirstRenderAsync()
    {
        return Task.CompletedTask;
    }
    protected virtual void Dispose(bool disposing)
    {
    }
    //the one on github did not.  i followed their pattern.
    void IDisposable.Dispose()
    {
        if (EditContext != null)
        {
            EditContext.OnValidationStateChanged -= _validationStateChangedHandler;
        }
        TabContainer.RemoveFocusItem(this);
        Dispose(disposing: true);
    }
}