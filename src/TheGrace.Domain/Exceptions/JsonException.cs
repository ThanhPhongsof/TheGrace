using TheGrace.Domain.Exceptions.Commons;

namespace TheGrace.Domain.Exceptions;

public static class JsonException
{
    public class JsonInvalidException : BadRequestException
    {
        public JsonInvalidException(string From) : base($"Invalid JSON for {From}") { }
    }
}
