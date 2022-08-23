﻿using ImportantDocuments.API.Domain;

namespace ImportantDocuments.API.Services
{
    public interface IDocService : IBaseService<Document>
    {
        public Task<Document> AddTagAsync(int id, Tag tag);
        public Task<Document> AddDocAsync(Document doc);
        public Task<bool> ContainsDocByIdAsync(int id);
    }
}