using ff2 = CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.FileFunctions.FileContentRegistry;
namespace BasicBlazorLibrary.Components.Basic;
public partial class ResourceImage
{
    [Parameter]
    public string Transform { get; set; } = "";
    [Parameter]
    public string Width { get; set; } = "";
    [Parameter]
    public string Height { get; set; } = "";
    [Parameter]
    public string X { get; set; } = "";
    [Parameter]
    public string Y { get; set; } = "";
    [Parameter]
    public string ID { get; set; } = "";
    [Parameter]
    [EditorRequired]
    public string FileName { get; set; } = "";
    [Parameter]
    public bool Fixed { get; set; } = true;
    private string GetLink()
    {
        return ff2.GetFile(FileName);
    }
    protected override bool ShouldRender()
    {
        return !Fixed; //hopefully this can speed up as well since you should not have to rerender this anyways.
    }
    private string GetFinalID => $"#{ID}";
}