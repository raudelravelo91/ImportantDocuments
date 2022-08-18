using ImportantDocuments.Domain;

namespace ImportantDocuments.API.Services
{
    public interface IDocService : IBaseService<Document>
    {
        public Task<Document> AddDocAsync(Document doc);
        public Task<bool> ContainsDocByIdAsync(int id);
    }
}