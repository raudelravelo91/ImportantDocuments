using ImportantDocuments.API.Domain;

namespace ImportantDocuments.API.Services
{
    public interface ITagService : IBaseService<Tag>
    {
        public Task<Tag> AddTagAsync(Tag tag);
        public Task<bool> ContainsTagByNameAsync(string name);
        public Task<bool> ContainsTagByIdAsync(int id);
        public Task<Tag> GetTagByNameAsync(string name);
    }
}