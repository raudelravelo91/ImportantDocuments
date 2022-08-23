using ImportantDocuments.API.Domain;
using Microsoft.EntityFrameworkCore;

namespace ImportantDocuments.API.Services;

public abstract class BaseService<TEntity> : IBaseService<TEntity> where TEntity : BaseModel
{
    private readonly ILogger _logger;
    private readonly IAppDbContext _context;
    protected IAppDbContext Context => _context;

    public BaseService(IAppDbContext context, ILogger logger)
    {
        _context = context;
        _logger = logger;
    }

    public abstract DbSet<TEntity> GetDbSet();
    
    public virtual async Task<TEntity> InsertAsync(TEntity obj)
    {
        var dbSet = GetDbSet();
        dbSet.Add(obj);

        await _context.CompleteAsync();
        return obj;
    }

    public void Detach(TEntity obj)
    {
        throw new NotImplementedException();
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await GetDbSet().ToListAsync();
    }

    public virtual async Task<TEntity> GetByIdAsync(params object[] pk)
    {
        var dbSet = GetDbSet();
        var obj = await dbSet.FindAsync(pk);
        if (obj == null)
            throw new Exception($"Entity with id {pk} not found");

        return obj;
    }

    public virtual async Task<TEntity> UpdateAsync(TEntity obj)
    {
        var dbSet = GetDbSet();
        dbSet.Update(obj);

        await _context.CompleteAsync();

        return obj;
    }

    public virtual async Task DeleteAsync(params object[] pk)
    {
        var _dbSet = GetDbSet();
        var obj = await GetByIdAsync(pk);
        _dbSet.Remove(obj);

        await _context.CompleteAsync();

    }
}