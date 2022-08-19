using ImportantDocuments.API.Domain;

namespace ImportantDocuments.DTOs
{
    public class DocumentCreationDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public LocationType LocationType { get; set; }
        public List<TagCreationDTO> Tags { get; set; }
    }
}
