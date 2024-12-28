namespace TheGrace.Application.Abstractions.Shared;

public interface IValidationResult
{
    Error[] Errors { get; }

    public static readonly Error ValidationError = new("Validation", "A Validation problem occurred");
}
