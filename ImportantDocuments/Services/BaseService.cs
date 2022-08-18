using ImportantDocuments.API.Domain;
using Microsoft.EntityFrameworkCore;

namespace ImportantDocuments.API.Services;

public abstract class BaseService<TEntity> : IBaseService<TEntity> where TEntity : BaseModel
{

    private readonly IAppDbContext _context;

    public BaseService(IAppDbContext context)
    {
        _context = context;
    }

    public abstract DbSet<TEntity> GetDbSet();
    

    public virtual async Task<TEntity> Insert(TEntity obj)
    {
        var dbSet = GetDbSet();
        dbSet.Add(obj);

        await _context.Complete();
        return obj;
    }

    public void Detach(TEntity obj)
    {
        throw new NotImplementedException();
    }

    public virtual async Task<IEnumerable<TEntity>> GetAll()
    {
        return await GetDbSet().ToListAsync();
    }

    public virtual async Task<TEntity> GetById(params object[] pk)
    {
        var dbSet = GetDbSet();
        var obj = await dbSet.FindAsync(pk);
        if (obj == null)
            throw new Exception($"Entity with id {pk} not found");

        return obj;
    }

    public virtual async Task<TEntity> Update(TEntity obj)
    {
        var dbSet = GetDbSet();
        dbSet.Update(obj);

        await _context.Complete();

        return obj;
    }

    public virtual async Task Delete(params object[] pk)
    {
        var _dbSet = GetDbSet();
        var obj = await GetById(pk);
        _dbSet.Remove(obj);

        await _context.Complete();

    }
}