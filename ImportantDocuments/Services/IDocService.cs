using ImportantDocuments.Domain;

namespace ImportantDocuments.Services
{
    public interface IDocService
    {
        public Task<Document> AddDocAsync(Document doc);
        public Task<IEnumerable<Document>> GetAllDocsAsync();
        public Task<bool> ContainsDocByIdAsync(int id);
        public Task<Document> GetDocByIdAsync(int id);
    }
}