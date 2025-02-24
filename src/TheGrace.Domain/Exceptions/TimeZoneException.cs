using TheGrace.Domain.Exceptions.Commons;

namespace TheGrace.Domain.Exceptions;

public static class TimeZoneException
{
    public class TimeZoneNotFoundException : NotFoundException
    {
        public TimeZoneNotFoundException() : base("The specified time zone string could not be found.") { }
    }

    public class TimeSpanIsValidException : BadRequestException
    {
        public TimeSpanIsValidException(string timeString) : base($"Invalid time format with time string {timeString}") { }
    }
}
