namespace TheGrace.Domain.Abstractions;

public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// Call save change from db context
    /// </summary>
    Task CommitAsync(CancellationToken cancellationToken = default);
}