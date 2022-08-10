using Microsoft.EntityFrameworkCore;
using ImportantDocuments.Domain;
using ImportantDocuments.Exceptions;
using ImportantDocuments.DTOs;

namespace ImportantDocuments.Services
{
    public class DocService : IDocService
    {
        private readonly AppDbContext _context;
        private readonly ILogger _logger;
        private readonly ITagService _tagService;

        public DocService(AppDbContext context, ILogger<DocService> logger, ITagService tagService)
        {
            _context = context;
            _logger = logger;
            _tagService = tagService;
        }

        public async Task<Document> AddDocAsync(Document doc)
        {
            try
            {
                // First add tags to DB and get all the IDs
                var tags = await AddTagsAsync(doc);
                
                // Then add the doc but now with tags containing their IDs
                // so that EF does not try to reinsert them
                doc.Tags.Clear();
                doc.Tags.AddRange(tags);

                var docDB = await _context.Documents.AddAsync(doc);
                _logger.LogInformation($"Doc added: {doc.Name}");
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Changes Saved in {nameof(AddDocAsync)}");
                return docDB.Entity;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error saving doc in {nameof(AddDocAsync)}", ex);
                throw;
            }
        }

        private async Task<List<Tag>> AddTagsAsync(Document doc)
        {
            var tags = new List<Tag>();
            
            if (doc.Tags.Count > 0)
            {
                foreach (var tag in doc.Tags)
                {
                    var tagDB = await _tagService.AddTagAsync(tag);
                    _logger.LogInformation($"Tag {tag.Name} added for: {doc.Name}");
                    tags.Add(tagDB);
                }
            }

            return tags;
        }

        public async Task<IEnumerable<Document>> GetAllDocsAsync()
        {
            return await _context.Documents
                    .ToListAsync();
        }

        public async Task<bool> ContainsDocByIdAsync(int id)
        {
            return await _context.Documents
                .AnyAsync(e => e.Id == id);
        }

        public async Task<Document> GetDocByIdAsync(int id)
        {
            var doc = await _context.Documents
                .Include(doc => doc.Tags)
                .FirstOrDefaultAsync(doc => doc.Id == id);
            
            if (doc == null)
            {
                throw new AppException($"Doc not found. Doc id: {id}");
            }

            return doc;
        }
    }
}
