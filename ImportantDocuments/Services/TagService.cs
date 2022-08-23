using ImportantDocuments.API.Domain;
using Microsoft.EntityFrameworkCore;

namespace ImportantDocuments.API.Services
{
    public class TagService : BaseService<Tag>, ITagService
    {
        private readonly ILogger _logger;

        public TagService(IAppDbContext context, ILogger<TagService> logger) : base(context, logger)
        {
            _logger = logger;
        }

        public async Task<Tag> AddTagAsync(Tag tag)
        {
            try
            {
                if (!await ContainsTagByNameAsync(tag.Name))
                {
                    var tagDb = await InsertAsync(tag);
                    _logger.LogInformation($"Changes Saved in {nameof(AddTagAsync)}");
                    return tagDb;
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

        public async Task<bool> ContainsTagByNameAsync(string name)
        {
            return await Context.Tags
                .AnyAsync(tag => tag.Name.ToLower().Equals(name.ToLower()));
        }

        public async Task<bool> ContainsTagByIdAsync(int id)
        {
            return await Context.Tags
                .AnyAsync(e => e.Id == id);
        }

        public async Task<Tag> GetTagByNameAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                // TODO: don't throw Exception, we should be wrapping all unhandled exceptions into our custom ones.
                throw new Exception($"Tag name can not be null or empty.");
            }

            var tag = await Context.Tags
                .Include(tag => tag.Documents)
                .FirstOrDefaultAsync(tag => tag.Name.ToLower().Equals(name.ToLower()));

            if (tag == null)
            {
                // TODO: don't throw Exception, we should be wrapping all unhandled exceptions into our custom ones.
                throw new Exception($"Tag not found. Tag name: {name}");
            }

            return tag;
        }

        protected override DbSet<Tag> GetDbSet()
        {
            return Context.Tags;
        }
    }
}
