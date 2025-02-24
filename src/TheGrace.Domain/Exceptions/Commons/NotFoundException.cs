namespace TheGrace.Domain.Exceptions.Commons;

public class NotFoundException : DomainException
{
    public NotFoundException(string message) : base("Not Found", message)
    { }
}
