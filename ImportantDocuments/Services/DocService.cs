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

        public DocService(AppDbContext context, ILogger<DocService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Document> AddDocAsync(Document doc)
        {
            try
            {
                if(doc.LocationType == 0)
                {

                }
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
