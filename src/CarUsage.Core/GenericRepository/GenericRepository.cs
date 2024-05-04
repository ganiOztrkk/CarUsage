using Microsoft.EntityFrameworkCore;

namespace CarUsage.Core.GenericRepository;

public class GenericRepository<TEntity, TContext>(TContext context) : IGenericRepository<TEntity>
    where TEntity : class, IEntity, new()
    where TContext : DbContext
{
    #region get
    public TEntity? Get(Guid id)
    {
        return context.Set<TEntity>().Find(id);
    }

    public async Task<TEntity?> GetAsync(Guid id)
    {
        return await context.Set<TEntity>().FindAsync(id);
    }
    #endregion

    #region getAll
    public IList<TEntity?> GetAll()
    {
        return context.Set<TEntity>().AsNoTracking().ToList()!;
    }

    public async Task<IList<TEntity?>> GetAllAsync()
    {
        return (await context.Set<TEntity>().AsNoTracking().ToListAsync())!;
    }
    #endregion

    #region insert
    public void Insert(TEntity entity)
    {
        context.Add(entity);
        context.SaveChanges();
    }

    public async Task InsertAsync(TEntity entity)
    {
        await context.AddAsync(entity);
        await context.SaveChangesAsync();
    }
    #endregion

    #region update
    public void Update(TEntity entity)
    {
        context.Update(entity);
        context.SaveChanges();
    }

    public async Task UpdateAsync(TEntity entity)
    {
        context.Update(entity);
        await context.SaveChangesAsync();
    }
    #endregion

    #region delete
    public void Delete(TEntity entity)
    {
        context.Remove(entity);
        context.SaveChanges();
    }

    public async Task DeleteAsync(TEntity entity)
    {
        context.Remove(entity);
        await context.SaveChangesAsync();
    }
    #endregion
}