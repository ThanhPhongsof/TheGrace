namespace TheGrace.Domain.Exceptions.Commons;

public class BadRequestException : DomainException
{
    public BadRequestException(string message) : base("Bad Request", message)
    { }
}
