using ff2 = CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.FileFunctions.FileContentRegistry;
namespace BasicBlazorLibrary.Components.Basic;

public partial class BackgroundImageContainer
{
    [Parameter] public string Name { get; set; } = "";
    [Parameter] public string? Class { get; set; }

    // Optional: let callers pass extra style (e.g., background-size, position, etc.)
    [Parameter] public string? Style { get; set; }

    [Parameter] public RenderFragment? ChildContent { get; set; }

    // Optional: allow attribute splatting (id, data-*, etc.)
    [Parameter(CaptureUnmatchedValues = true)]
    public IReadOnlyDictionary<string, object>? AdditionalAttributes { get; set; }

    private string CssClass => string.IsNullOrWhiteSpace(Class) ? "" : Class!.Trim();

    private string FinalStyle
    {
        get
        {
            // If Name is blank, we just render whatever Style is.
            if (string.IsNullOrWhiteSpace(Name))
            {
                return Style ?? "";
            }

            var raw = ff2.GetFile(Name);

            // IMPORTANT: quoting inside url('...') keeps data: urls + svg+xml happy in CSS.
            // Also works for normal http/https paths.
            var bg = $"background-image:url('{raw}');";

            if (string.IsNullOrWhiteSpace(Style))
            {
                return bg;
            }

            // Ensure we have a trailing semicolon before appending
            var s = Style!.Trim();
            if (!s.EndsWith(";"))
            {
                s += ";";
            }
            return bg + s;
        }
    }
}