namespace TheGrace.Domain.Abstractions.Entities;

public interface IEntityAudit<T> : IEntity<T>, IAuditTable
{
}
