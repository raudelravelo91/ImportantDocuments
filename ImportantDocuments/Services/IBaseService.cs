using ImportantDocuments.API.Domain;
using Microsoft.EntityFrameworkCore;

namespace ImportantDocuments.API.Services;

public interface IBaseService<TEntity> where TEntity : BaseModel
{
    Task Delete(params object[] pk);
    Task<TEntity> Insert(TEntity obj);
    Task<TEntity> GetById(params object[] pk);
    void Detach(TEntity obj);
    Task<IEnumerable<TEntity>> GetAll();
    Task<TEntity> Update(TEntity obj);
    DbSet<TEntity> GetDbSet();
    
}