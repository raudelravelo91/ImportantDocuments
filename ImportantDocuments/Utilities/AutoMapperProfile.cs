using AutoMapper;
using ImportantDocuments.Domain;
using ImportantDocuments.DTOs;

namespace ImportantDocuments.Utilities
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Tag, TagDTO>();
            CreateMap<Tag, TagReadDTO>();
            CreateMap<TagCreationDTO, Tag>();
            CreateMap<Document, TagDocDTO>();
            CreateMap<DocumentCreationDTO, Document>();
            CreateMap<Document, DocumentDTO>();
            CreateMap<Document, DocumentReadDTO>();
            CreateMap<Tag, DocTagDTO>();
        }
    }
}
