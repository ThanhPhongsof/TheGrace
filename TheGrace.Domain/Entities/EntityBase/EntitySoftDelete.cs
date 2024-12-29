using TheGrace.Domain.Abstractions.Entities;

namespace TheGrace.Domain.Entities.EntityBase;

public class EntitySoftDelete<T> : Entity<T>, ISoftDelete
{
    public bool IsInActive { get; set; }
}
