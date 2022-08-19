using ImportantDocuments.API.Domain;

namespace ImportantDocuments.API.Services;

public interface IDocService: IBaseService<Document>
{
    public Task<bool> ContainsDocByIdAsync(int id);
}