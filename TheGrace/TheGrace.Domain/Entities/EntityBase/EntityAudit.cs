using TheGrace.Domain.Abstractions.Entities;

namespace TheGrace.Domain.Entities.EntityBase;

public class EntityAudit<T> : Entity<T>, IEntityAudit<T>, ISoftDelete
{
    public bool IsInActive { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset UpdatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }
}

public class EntityAudit : IDateTracking, IUserTracking, ISoftDelete
{
    public bool IsInActive { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset UpdatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }
}
