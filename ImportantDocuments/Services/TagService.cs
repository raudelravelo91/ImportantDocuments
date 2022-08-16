using ImportantDocuments.API;
using ImportantDocuments.API.Exceptions;
using Microsoft.EntityFrameworkCore;
using ImportantDocuments.Domain;

namespace ImportantDocuments.Services
{
    public class TagService : ITagService
    {
        private readonly AppDbContext _context;
        private readonly ILogger _logger;

        public TagService(AppDbContext context, ILogger<TagService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Tag> AddTagAsync(Tag tag)
        {
            try
            {
                if (!await ContainsTagByNameAsync(tag.Name))
                {
                    var tagDB = await _context.Tags.AddAsync(tag);
                    _logger.LogInformation($"Tag added: {tag.Name}");
                    await _context.SaveChangesAsync();
                    _logger.LogInformation($"Changes Saved in {nameof(AddTagAsync)}");
                    return tagDB.Entity;
                }

                _logger.LogInformation($"Tag not added as it already existed: {tag.Name}");
                return await GetTagByNameAsync(tag.Name);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error adding tag in {nameof(AddTagAsync)}", ex);
                throw;
            }
        }

        //Only tag names, no documents
        public async Task<IEnumerable<Tag>> GetAllTagsAsync()
        {
            return await _context.Tags
                    .ToListAsync();
        }

        public async Task<bool> ContainsTagByNameAsync(string name)
        {
            return await _context.Tags
                .AnyAsync(tag => tag.Name.ToLower().Equals(name.ToLower()));
        }

        public async Task<bool> ContainsTagByIdAsync(int id)
        {
            return await _context.Tags
                .AnyAsync(e => e.Id == id);
        }

        public async Task<Tag> GetTagByNameAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                // TODO: don't throw Exception, we should be wrapping all unhandled exceptions into our custom ones.
                throw new Exception($"Tag name can not be null or empty.");
            }

            var tag = await _context.Tags
                .Include(tag => tag.Documents)
                .FirstOrDefaultAsync(tag => tag.Name.ToLower().Equals(name.ToLower()));

            if (tag == null)
            {
                // TODO: don't throw Exception, we should be wrapping all unhandled exceptions into our custom ones.
                throw new Exception($"Tag not found. Tag name: {name}");
            }

            return tag;
        }

        public async Task<Tag> GetTagByIdAsync(int id)
        {
            var tag = await _context.Tags
                .Include(tag => tag.Documents)
                .FirstOrDefaultAsync(tag => tag.Id == id);

            if (tag == null)
            {
                throw new Exception($"Tag not found. Tag id: {id}");
            }

            return tag;
        }
    }
}
