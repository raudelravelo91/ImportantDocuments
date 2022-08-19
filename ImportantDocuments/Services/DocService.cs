using ImportantDocuments.API.Services;
using ImportantDocuments.API.Exceptions;
using Microsoft.EntityFrameworkCore;
using ImportantDocuments.API.Domain;

namespace ImportantDocuments.Services
{
    public class DocService : IDocService
    {
        private readonly ILogger _logger;
        private readonly ITagService _tagService;
        private readonly IBaseService<Document> _baseService;

        public DocService(ILogger<DocService> logger, ITagService tagService, IBaseService<Document> baseService)
        {
            _logger = logger;
            _tagService = tagService;
            _baseService = baseService;
        }

        private async Task<List<Tag>> AddTagsAsync(Document doc)
        {
            var tags = new List<Tag>();

            if (doc.Tags.Count <= 0) return tags;
            foreach (var tag in doc.Tags)
            {
                var tagDB = await _tagService.AddTagAsync(tag);
                tags.Add(tagDB);
            }

            return tags;
        }

        public async Task<bool> ContainsDocByIdAsync(int id)
        {
            return await _baseService.GetDbSet()
                .AnyAsync(e => e.Id == id);
        }

        public async Task DeleteAsync(params object[] pk)
        {
            await _baseService.DeleteAsync(pk);
        }

        public async Task<Document> InsertAsync(Document doc)
        {
            try
            {
                // First add tags to DB and get all the IDs
                var tags = await AddTagsAsync(doc);

                // Then add the doc but now with tags containing their IDs
                // so that EF does not try to reinsert them
                doc.Tags.Clear();
                doc.Tags.AddRange(tags);

                var docDB = await _baseService.InsertAsync(doc);
                return docDB;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error saving doc.", ex);
                throw;
            }
        }

        public async Task<Document> GetByIdAsync(params object[] pk)
        {
            return await _baseService.GetByIdAsync();
        }

        public void Detach(Document doc)
        {
            _baseService.Detach(doc);
        }

        public async Task<IEnumerable<Document>> GetAllAsync()
        {
            return await _baseService.GetAllAsync();
        }

        public async Task<Document> UpdateAsync(Document doc)
        {
            return await _baseService.UpdateAsync(doc);
        }

        public DbSet<Document> GetDbSet()
        {
            return _baseService.GetDbSet();
        }
    }
}
