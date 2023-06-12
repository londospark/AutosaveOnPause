using System;

namespace AutosaveOnPause;

public static class DateTimeExtensions
{
    public static DateTime SubtractMinutes(this DateTime dt, double minutes) => dt.AddMinutes(-1 * minutes);
}