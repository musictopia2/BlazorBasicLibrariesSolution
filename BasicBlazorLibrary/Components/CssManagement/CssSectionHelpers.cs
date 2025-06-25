namespace BasicBlazorLibrary.Components.CssManagement;
/// <summary>
/// Provides constants for CSS section names used in the application's layout.
/// These sections represent logical groupings of CSS to be included in the HTML &lt;head&gt;.
/// </summary>
internal static class CssSectionHelpers
{
    /// <summary>
    /// Section for preloading CSS files that should be loaded before anything else.
    /// Use this for fonts, resets, or critical CSS.
    /// </summary>
    internal static string PreloadCssSection => "preloadcss";

    /// <summary>
    /// Section for custom CSS that should be included before the base application-wide styles.
    /// Useful for styles that bootstrap or app.css might override.
    /// </summary>
    internal static string PreBaseCssSection => "prebasecss";

    /// <summary>
    /// Section for base application-wide CSS files (e.g., app.css).
    /// This is the main foundational CSS that most components will rely on.
    /// </summary>
    internal static string BaseCssSection => "startcss";

    /// <summary>
    /// Section for CSS overrides targeting components.
    /// This is intended for component-specific styling that should override base styles.
    /// </summary>
    internal static string ComponentCssSection => "componentcss";

    /// <summary>
    /// Section for CSS to be included immediately after component CSS.
    /// Useful for final adjustments and overrides specific to components.
    /// </summary>
    internal static string PostComponentCssSection => "postcomponentcss";

    /// <summary>
    /// Section for CSS that loads after scoped component CSS files.
    /// Allows final overriding or theming on a per-app basis.
    /// </summary>
    internal static string PostScopedCssSection => "postscopedcss";
}