using CommonBasicLibraries.BasicUIProcesses; //i think
namespace BasicBlazorLibrary.Components.Basic;
public partial class WorkProgressComponent<TValue>
{
    private enum EnumStatus
    {
        NoneToBegin,
        Progress,
        Completed
    }
    [Inject]
    private IToast? Toast { get; set; }
    [Parameter]
    public RenderFragment<TValue>? WorkContent { get; set; }
    [Parameter]
    public RenderFragment? CompletedContent { get; set; }
    [Parameter]
    public string WorkTitle { get; set; } = "Processing";
    [Parameter]
    public RenderFragment? NoneContent { get; set; }
    [Parameter]
    public EventCallback OnNoneToBeginWith { get; set; }
    [Parameter]
    public EventCallback<TValue> OnContinueOn { get; set; }
    [Parameter]
    public EventCallback<TValue> OnBeginningOfProcess { get; set; }
    [Parameter]
    public EventCallback OnCompleted { get; set; }
    private bool _didChange;
    private bool _loading = true;
    private int _index;
    private EnumStatus _status = EnumStatus.NoneToBegin;
    private BasicList<TValue> _itemList = new();
    [Parameter]
#pragma warning disable BL0007 // Component parameters should be auto properties
    public BasicList<TValue> ItemList
#pragma warning restore BL0007 // Component parameters should be auto properties
    {
        get => _itemList;
        set
        {
            if (value != _itemList)
            {
                _didChange = true;
                _itemList = value;
            }
        }
    }
    protected override void OnInitialized()
    {
        _didChange = true;
        base.OnInitialized();
    }
    protected override async Task OnParametersSetAsync()
    {
        if (_didChange == false)
        {
            _loading = false;
            return;
        }
        if (ItemList.Count == 0 && OnNoneToBeginWith.HasDelegate)
        {
            await OnNoneToBeginWith.InvokeAsync();
            _loading = false;
            _didChange = false;
            return;
        }
        if (ItemList.Count == 0)
        {
            throw new CustomBasicException("Must have a method to run when there is none to begin with");
        }
        _status = EnumStatus.Progress;
        _index = 0;
        _loading = false;
        _didChange = false;
        if (OnContinueOn.HasDelegate)
        {
            await OnContinueOn.InvokeAsync(ItemList.First());
        }
    }
    public async Task NextOneAsync()
    {
        if (_index == ItemList.Count - 1)
        {
            await PrivateCompletedAsync();
            return;
        }
        _index++;
        await PrivateContinueAsync();
    }
    private int UpTo => _index + 1;
    private async Task PrivateContinueAsync()
    {
        if (OnContinueOn.HasDelegate)
        {
            await OnContinueOn.InvokeAsync(ItemList[_index]);
            return;
        }
    }
    private async Task PrivateCompletedAsync()
    {
        _status = EnumStatus.Completed;
        _index = -1;
        if (OnCompleted.HasDelegate)
        {
            await OnCompleted.InvokeAsync();
        }
    }
    public async Task PreviousOneAsync()
    {
        if (_index < 1)
        {
            Toast!.ShowUserErrorToast("Cannot go to previous one because you are already at the beginning");
            return;
        }
        _index--;
        await PrivateContinueAsync();
    }
    public async Task SkipSeveralAsync(int howMany)
    {
        if (_index + howMany + 1 >= ItemList.Count)
        {
            await PrivateCompletedAsync();
            return;
        }
        _index += howMany;
        await PrivateContinueAsync();
    }
}