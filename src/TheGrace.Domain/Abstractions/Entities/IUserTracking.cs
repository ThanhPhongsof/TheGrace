namespace TheGrace.Domain.Abstractions.Entities;

public interface IUserTracking
{
    string? CreatedBy { get; set; }

    string? UpdatedBy { get; set; }
}
