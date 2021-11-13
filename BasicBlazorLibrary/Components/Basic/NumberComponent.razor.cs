namespace BasicBlazorLibrary.Components.Basic;
public partial class NumberComponent<TValue>
{
    private readonly static string _stepAttributeValue;
    static NumberComponent()
    {
        var targetType = Nullable.GetUnderlyingType(typeof(TValue)) ?? typeof(TValue);
        if (targetType == typeof(int) ||
            targetType == typeof(long) ||
            targetType == typeof(short) ||
            targetType == typeof(float) ||
            targetType == typeof(double) ||
            targetType == typeof(decimal))
        {
            _stepAttributeValue = "any";
        }
        else
        {
            throw new InvalidOperationException($"The type '{targetType}' is not a supported numeric type.");
        }
    }
    [Parameter]
    public string Width { get; set; } = "75px";
}