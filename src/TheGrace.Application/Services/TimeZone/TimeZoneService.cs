using TheGrace.Domain.Exceptions;

namespace TheGrace.Application.Services.TimeZone;

public class TimeZoneService : ITimeZoneService
{
    private readonly TimeZoneInfo _globalTimeZone;

    public TimeZoneService(string timeZoneId)
    {
        if (string.IsNullOrEmpty(timeZoneId))
        {
            throw new TimeZoneException.TimeZoneNotFoundException();
        }

        _globalTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
    }

    public DateTimeOffset ConvertToGlobalTime(DateTimeOffset dateTime)
    {
        var newOffset = _globalTimeZone.GetUtcOffset(dateTime.UtcDateTime);
        return new DateTimeOffset(dateTime.DateTime, newOffset);
    }

    public DateTimeOffset GetCurrentTime()
    {
        return TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow, _globalTimeZone);
    }

    public TimeSpan ConvertToTimeSpan(string timeString)
    {
        if (string.IsNullOrEmpty(timeString))
        {
            return TimeSpan.Zero;
        }

        string[] formats = { @"hh\:mm", @"hh\:mm\:ss" };
        bool success = TimeSpan.TryParseExact(timeString, formats, null, out TimeSpan time);

        if (!success)
        {
            throw new TimeZoneException.TimeSpanIsValidException(timeString);
        }

        return time;
    }
}
