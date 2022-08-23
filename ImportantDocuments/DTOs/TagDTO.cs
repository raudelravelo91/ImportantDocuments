using ImportantDocuments.DTOs;

namespace ImportantDocuments.API.DTOs
{
    public class TagDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<TagDocDTO> Documents { get; set; }
    }
}
