namespace TheGrace.Application.Services.V1.TimeZone;

public interface ITimeZoneService
{
    DateTimeOffset GetCurrentTime();

    DateTimeOffset ConvertToGlobalTime(DateTimeOffset dateTime);

    TimeSpan ConvertToTimeSpan(string timeString);
}
