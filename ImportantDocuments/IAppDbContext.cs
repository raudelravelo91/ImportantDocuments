using ImportantDocuments.API.Domain;
using Microsoft.EntityFrameworkCore;

namespace ImportantDocuments.API;

public interface IAppDbContext
{
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Document> Documents { get; set; }
    public DbSet<DocumentTag> DocumentTags { get; set; }
    Task CompleteAsync();
}