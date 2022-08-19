using ImportantDocuments.API.Domain;

namespace ImportantDocuments.DTOs;

public class DocumentDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    public LocationType LocationType { get; set; }
    public List<DocTagDTO> Tags { get; set; } 
}
