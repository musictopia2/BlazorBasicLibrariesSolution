using ff2 = CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.FileFunctions.FileContentRegistry;
namespace BasicBlazorLibrary.Components.Basic;
public partial class ImageButtonComponent
{
    public static bool FromResource { get; set; } //i think if set to true, should apply to all.
    // Name-based source resolution
    [Parameter] public string Name { get; set; } = "";
    [Parameter] public string BasePath { get; set; } = "/";

    // Legacy/explicit
    [Parameter] public string MainSource { get; set; } = "";
    [Parameter] public string HoverSource { get; set; } = "";

    // NEW: int pixel sizing (matches <img width=".." height="..">)
    [Parameter] public int? Width { get; set; }
    [Parameter] public int? Height { get; set; }

    // NEW: standard img attributes
    [Parameter] public string? Alt { get; set; }
    [Parameter] public string? Class { get; set; }
    [Parameter] public string? Style { get; set; }

    // Back-compat
    [Parameter] public string MarginLeft { get; set; } = "2px";



    [Parameter]
    public EventCallback OnClick { get; set; }

    private ElementReference _element;
    private HoverClass? _hover;

    // Computed sources (so callers can use either Name or explicit sources)
    private string ResolvedMainSource =>
        !string.IsNullOrWhiteSpace(MainSource)
            ? MainSource
            : ResolveFromName(Name);

    private string ResolvedHoverSource =>
        !string.IsNullOrWhiteSpace(HoverSource)
            ? HoverSource
            : ""; // no hover by default for Name mode


    private string ResolveFromName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return "";
        }

        if (FromResource)
        {
            return ff2.GetFile(name);
        }


        // Ensure BasePath ends with /
        var basePath = string.IsNullOrWhiteSpace(BasePath) ? "/" : BasePath;
        if (!basePath.EndsWith('/'))
        {
            basePath += "/";
        }

        return $"{basePath}{name}.png";
    }

    protected override void OnInitialized()
    {
        _element = default;
        _hover = new(JS!);
        base.OnInitialized();
    }
    private async Task HoverAsync()
    {
        // Only do hover if hover source exists
        if (string.IsNullOrWhiteSpace(ResolvedHoverSource))
        {
            return;
        }

        await _hover!.HoverAsync(_element, ResolvedHoverSource);
    }

    private async Task UnhoverAsync()
    {
        if (string.IsNullOrWhiteSpace(ResolvedHoverSource))
        {
            return;
        }

        await _hover!.UnHoverAsync(_element, ResolvedMainSource);
    }

    private string BuildStyle()
    {
        // Only build style from: margin-left (back-compat), cursor, and custom Style.
        var parts = new BasicList<string>();

        if (!string.IsNullOrWhiteSpace(MarginLeft) &&
            (string.IsNullOrWhiteSpace(Style) || !Style.Contains("margin-left", StringComparison.OrdinalIgnoreCase)))
        {
            parts.Add($"margin-left: {MarginLeft}");
        }

        if (string.IsNullOrWhiteSpace(Style) || !Style.Contains("cursor", StringComparison.OrdinalIgnoreCase))
        {
            parts.Add("cursor: pointer");
        }
        parts.Add("pointer-events:auto;");
        if (!string.IsNullOrWhiteSpace(Style))
        {
            parts.Add(Style.Trim().TrimEnd(';'));
        }
        
        return parts.Count == 0 ? "" : string.Join("; ", parts) + ";";
    }

}