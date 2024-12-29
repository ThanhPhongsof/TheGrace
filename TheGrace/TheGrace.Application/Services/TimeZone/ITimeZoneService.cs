namespace TheGrace.Application.Services.TimeZone;

public interface ITimeZoneService
{
    DateTimeOffset GetCurrentTime();

    DateTimeOffset ConvertToGlobalTime(DateTimeOffset dateTime);

    TimeSpan ConvertToTimeSpan(string timeString);
}
