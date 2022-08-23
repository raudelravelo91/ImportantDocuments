using ImportantDocuments.API.Domain;
using Microsoft.EntityFrameworkCore;

namespace ImportantDocuments.API.Services;

public interface IBaseService<TEntity> where TEntity : BaseModel
{
    Task DeleteAsync(params object[] pk);
    Task<TEntity> InsertAsync(TEntity obj);
    Task<TEntity> GetByIdAsync(params object[] pk);
    void Detach(TEntity obj);
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity> UpdateAsync(TEntity obj);
}