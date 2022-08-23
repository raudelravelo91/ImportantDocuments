using AutoMapper;
using ImportantDocuments.API.Domain;
using ImportantDocuments.API.DTOs;
using ImportantDocuments.DTOs;

namespace ImportantDocuments.API.Utilities
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
            CreateMap<Document, DocumentPutDto>();
            CreateMap<DocumentPutDto, Document>();
            CreateMap<Tag, DocTagDTO>();
            CreateMap<Tag, TagPutDTO>();
            CreateMap<TagPutDTO, Tag>();
        }
    }
}
