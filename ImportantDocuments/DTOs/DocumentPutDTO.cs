using ImportantDocuments.API.Domain;

namespace ImportantDocuments.API.DTOs;

public class DocumentPutDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    public LocationType LocationType { get; set; }
}