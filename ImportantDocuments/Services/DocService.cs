﻿using ImportantDocuments.Domain;
using ImportantDocuments.Services;
using Microsoft.EntityFrameworkCore;

namespace ImportantDocuments.API.Services
{
    public class DocService :  BaseService<Document>, IDocService
    {
        private readonly ILogger _logger;
        private readonly ITagService _tagService;

        public DocService(IAppDbContext context, ILogger<DocService> logger, ITagService tagService) : base(context, logger)
        {
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

                var docDB = await InsertAsync(doc);
                _logger.LogInformation($"Changes Saved in {nameof(AddDocAsync)}");
                return docDB;
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

            if (doc.Tags.Count <= 0) return tags;
            foreach (var tag in doc.Tags)
            {
                var tagDB = await _tagService.AddTagAsync(tag);
                _logger.LogInformation($"Tag {tag.Name} added for: {doc.Name}");
                tags.Add(tagDB);
            }

            return tags;
        }

        public async Task<bool> ContainsDocByIdAsync(int id)
        {
            return await Context.Documents
                .AnyAsync(e => e.Id == id);
        }

        public Task<Document> AddTagAsync(int docId, Tag tag)
        {
            throw new NotImplementedException();
        }

        public override DbSet<Document> GetDbSet()
        {
            return Context.Documents;
        }
    }
}
