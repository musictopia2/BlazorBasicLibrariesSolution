namespace BasicBlazorLibrary.BasicJavascriptClasses;

/// <summary>
/// Provides functionality to suppress default browser behavior for function keys (F1–F11) and arrow keys.
/// Useful in interactive Blazor components where key handling should be customized.
/// </summary>
public class KeySuppressorClass(IJSRuntime js) : BaseLibraryJavascriptClass(js)
{
    /// <inheritdoc/>
    protected override string JavascriptFileName => "keySuppressor.js";

    /// <summary>
    /// Disables the default browser behavior for function keys (F1 to F11).
    /// F12 remains enabled to allow developer tools access.
    /// </summary>
    public async Task DisableFunctionKeysAsync()
    {
        await ModuleTask.InvokeVoidFromClassAsync("disableFunctionKeys");
    }

    /// <summary>
    /// Disables the default behavior of arrow keys (scrolling).
    /// </summary>
    public async Task DisableArrowKeysAsync()
    {
        await ModuleTask.InvokeVoidFromClassAsync("disableArrowKeys");
    }

    /// <summary>
    /// Enables function key behavior again. Called automatically during disposal.
    /// </summary>
    private async Task EnableFunctionKeysAsync()
    {
        await ModuleTask.InvokeVoidFromClassAsync("enableFunctionKeys");
    }

    /// <summary>
    /// Enables arrow key behavior again. Called automatically during disposal.
    /// </summary>
    private async Task EnableArrowKeysAsync()
    {
        await ModuleTask.InvokeVoidFromClassAsync("enableArrowKeys");
    }

    private bool _disposed = false;

    /// <summary>
    /// Ensures suppressed keys are re-enabled when the object is disposed.
    /// </summary>
    protected override async ValueTask DisposeAsyncCore()
    {
        if (!_disposed)
        {
            try
            {
                await EnableFunctionKeysAsync();
                await EnableArrowKeysAsync();
            }
            catch
            {
                // Optional: log or ignore dispose errors
            }

            _disposed = true;
        }

        await base.DisposeAsyncCore();
    }
}