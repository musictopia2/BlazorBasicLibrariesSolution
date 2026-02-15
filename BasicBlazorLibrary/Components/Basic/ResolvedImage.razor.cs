using ff2 = CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.FileFunctions.FileContentRegistry;

namespace BasicBlazorLibrary.Components.Basic;

public partial class ResolvedImage
{
    [Parameter, EditorRequired]
    public string Name { get; set; } = "";

    // Optional but good accessibility hygiene
    [Parameter]
    public string? Alt { get; set; }

    [Parameter(CaptureUnmatchedValues = true)]
    public IReadOnlyDictionary<string, object>? AdditionalAttributes { get; set; }

    private string Src => ff2.GetFile(Name);
}