namespace SampleToastServerSide.Shared;
public partial class SampleComponent
{
    [Inject]
    public ISampleViewModel? DataContext { get; set; }
    public void RunTest()
    {
        DataContext!.RunTest();
    }
}