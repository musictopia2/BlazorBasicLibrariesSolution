namespace SampleToastServerSide.Shared;
public partial class SampleComponent
{
    [Inject]
    public ISampleViewModel? DataContext { get; set; }
    public void TestToast()
    {
        DataContext!.TestToast();
    }
    public async Task TestMessageAsync()
    {
        await DataContext!.TestMessageBoxAsync();
    }
}