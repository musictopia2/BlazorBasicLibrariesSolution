namespace BasicBlazorLibrary.Components.CalendarPopups;
internal static class DateTimeDayOfMonthExtensions
{
    public static DateOnly FirstDayOfMonth(this DateOnly value)
    {
        return new DateOnly(value.Year, value.Month, 1);
    }
    public static int DaysInMonth(this DateOnly value)
    {
        return DateTime.DaysInMonth(value.Year, value.Month);
    }
    public static DateOnly LastDayOfMonth(this DateOnly value)
    {
        return new DateOnly(value.Year, value.Month, value.DaysInMonth());
    }
    public static string FirstDayStringMonth(this DateOnly value)
    {
        string monthName = value.ToString("MMMM");
        return $"{monthName} {value.Year}";
    }
    public static int DayOfWeekColumn(this DayOfWeek day)
    {
        return (int)day + 1;
    }
    public static string DayOfWeekShort(this DayOfWeek day)
    {
        string firstStr = day.ToString();
        return firstStr.Substring(0, 3);
    }
}