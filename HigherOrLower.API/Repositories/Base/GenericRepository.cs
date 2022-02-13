using Microsoft.EntityFrameworkCore;

namespace HigherOrLower.API.Repository.Base;

// a generic repository class that can be used for any entity
public class GenericRepository<T> where T : class
{
    private readonly HigherLowerDbContext _context;
    protected readonly DbSet<T> DbSet;

    public GenericRepository(HigherLowerDbContext context)
    {
        _context = context;
        DbSet = _context.Set<T>();
    }
    
    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await DbSet.ToListAsync();
    }
    
    public virtual async Task<T?> GetByIdAsync(Guid id)
    {
        return await DbSet.FindAsync(id);
    }

    public virtual async Task AddAsync(T entity)
    {
        await DbSet.AddAsync(entity);
    }
    
    public virtual async Task AddRangeAsync(IEnumerable<T> entities)
    {
        await DbSet.AddRangeAsync(entities);
    }

    public virtual void Update(T entity)
    {
        DbSet.Update(entity);
    }
    
    public virtual void UpdateRange(IEnumerable<T> entities)
    {
        DbSet.UpdateRange(entities);
    }

    public virtual void Delete(Guid id)
    {
        var entity = DbSet.Find(id);
        
        if (entity != null)
        {
            DbSet.Remove(entity);
        }
    }
    
    public virtual async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
