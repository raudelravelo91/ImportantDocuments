using ImportantDocuments.Domain;

namespace ImportantDocuments.Services
{
    public interface ITagService
    {
        public Task<Tag> AddTagAsync(Tag tag);
        public Task<IEnumerable<Tag>> GetAllTagsAsync();
        public Task<bool> ContainsTagByNameAsync(string name);
        public Task<bool> ContainsTagByIdAsync(int id);
        public Task<Tag> GetTagByNameAsync(string name);
        public Task<Tag> GetTagByIdAsync(int id);
    }
}