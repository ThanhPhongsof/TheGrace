namespace TheGrace.Domain.Abstractions.Entities;

public interface ISoftDelete
{
    bool IsInActive { get; set; }
}
