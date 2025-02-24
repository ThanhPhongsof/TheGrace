using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TheGrace.Domain.Abstractions.Repositories;
using TheGrace.Domain.Entities.EntityBase;

namespace TheGrace.Persistence.Repositories;

public class RepositoryBase<TEntity, TKey> : IRepositoryBase<TEntity, TKey> where TEntity : Entity<TKey>
{
    private readonly ApplicationDbContext _context;

    public RepositoryBase(ApplicationDbContext context) => _context = context;

    public void Dispose() => _context.Dispose();

    public IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>>? predicate = null,
        params Expression<Func<TEntity, object>>[] includeProperties)
    {
        IQueryable<TEntity> items = _context.Set<TEntity>().AsNoTracking(); // Importance Always include AsNoTracking for Query Side
        if (includeProperties != null)
            foreach (var includeProperty in includeProperties)
                items = items.Include(includeProperty);

        if (predicate is not null)
            items = items.Where(predicate);

        return items;
    }

    public async Task<TEntity> FindByIdAsync(TKey id, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includeProperties)
        => await FindAll(null, includeProperties).AsTracking().SingleOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);

    public async Task<TEntity> FindSingleAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includeProperties)
        => await FindAll(null, includeProperties).AsTracking().SingleOrDefaultAsync(predicate, cancellationToken);

    public void Add(TEntity entity)
        => _context.Add(entity);

    public void Remove(TEntity entity)
        => _context.Set<TEntity>().Remove(entity);

    public void Update(TEntity entity)
        => _context.Set<TEntity>().Update(entity);

    public void AddMultiple(List<TEntity> entities)
    {
        int iTotalCount = entities.Count,
            iTake = 1000, iEntityTake = iTake, iSkip = 0;

        if (iTotalCount == 0)
        {
            return;
        }

        while (iSkip < iTotalCount)
        {
            var entityTake = entities.Skip(iSkip).Take(iEntityTake).ToList();
            _context.Set<TEntity>().AddRange(entityTake);
            iSkip += iTake;

            if (iSkip + iTake > iTotalCount)
            {
                iEntityTake = iTotalCount - iSkip;
            }
        }
        _context.SaveChanges();
    }

    public void UpdateMultiple(List<TEntity> entities)
    {
        int iTotalCount = entities.Count,
            iTake = 1000, iEntityTake = iTake, iSkip = 0;

        if (iTotalCount == 0)
        {
            return;
        }

        while (iSkip < iEntityTake)
        {
            var entityTake = entities.Skip(iSkip).Take(iEntityTake).ToList();
            _context.Set<TEntity>().UpdateRange(entityTake);
            iSkip += iTake;

            if (iSkip + iTake > iTotalCount)
            {
                iEntityTake = iTotalCount - iSkip;
            }
        }
    }

    public void RemoveMultiple(List<TEntity> entities)
    {
        int iTotalCount = entities.Count,
            iTake = 1000, iEntityTake = iTake, iSkip = 0;

        if (iTotalCount == 0)
        {
            return;
        }

        while (iSkip < iEntityTake)
        {
            var entityTake = entities.Skip(iSkip).Take(iEntityTake).ToList();
            _context.Set<TEntity>().RemoveRange(entityTake);
            iSkip += iTake;

            if (iSkip + iTake > iTotalCount)
            {
                iEntityTake = iTotalCount - iSkip;
            }
        }
    }
}
