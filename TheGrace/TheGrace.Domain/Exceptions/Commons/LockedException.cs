namespace TheGrace.Domain.Exceptions.Commons;

public class LockedException : DomainException
{
    public LockedException(string message) : base("Locked", message)
    { }
}
