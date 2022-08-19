using ImportantDocuments.API.Domain;
using Microsoft.EntityFrameworkCore;

namespace ImportantDocuments.API
{
    public class AppDbContext : DbContext
    {
        private readonly ILogger<AppDbContext> _logger;
        public AppDbContext(DbContextOptions options, ILogger<AppDbContext> logger) : base(options)
        {
            _logger = logger;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Logging entity framework core queries
            optionsBuilder.LogTo(s => _logger.LogInformation(s));
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DocumentTag>()
                .HasKey(dt => new { dt.DocumentId, dt.TagId });

            modelBuilder.Entity<Document>()
                .HasMany(d => d.Tags)
                .WithMany(t => t.Documents)
                .UsingEntity<DocumentTag>(
                    dt => dt.HasOne(prop => prop.Tag).WithMany().HasForeignKey(prop => prop.TagId),
                    dt => dt.HasOne(prop => prop.Document).WithMany().HasForeignKey(prop => prop.DocumentId),
                    dt => dt.HasKey(prop => new {prop.TagId, prop.DocumentId})
                );

            modelBuilder.Entity<Tag>().HasData(
                new Tag {Id = 1, Name = "ImportantDocument"},
                new Tag {Id = 2, Name = "Banking"},
                new Tag {Id = 3, Name = "Tax"},
                new Tag {Id = 4, Name = "IdentityDocument"},
                new Tag {Id = 5, Name = "Work"}
            );

            modelBuilder.Entity<Document>().HasData(
                new Document {Id = 1, Name = "DocumentName1", LocationType = LocationType.None},
                new Document
                {
                    Id = 2, Name = "DocumentName2", Location = "C:/physicalPath",
                    LocationType = LocationType.PhysicalPath
                },
                new Document
                    {Id = 3, Name = "DocumentName3", Location = "www.google.ca", LocationType = LocationType.URL},
                new Document
                {
                    Id = 4, Name = "DocumentName3", Description = "Some test description",
                    LocationType = LocationType.None
                }
            );

            modelBuilder.Entity<DocumentTag>().HasData(
                new DocumentTag {DocumentId = 1, TagId = 1}
            );
        }


        public DbSet<Tag> Tags { get; set; }

        public DbSet<Document> Documents { get; set; }

        public DbSet<DocumentTag> DocumentTags { get; set; }
    }
}