using TheGrace.Domain.Abstractions.Entities;

namespace TheGrace.Domain.Entities.EntityBase;

public class Entity<T> : IEntity<T>
{
    public T Id { get; set; }
}
