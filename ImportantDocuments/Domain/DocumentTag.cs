namespace ImportantDocuments.API.Domain;

public class DocumentTag
{
    public int DocumentId { get; set; }
    public int TagId { get; set; }

    public Document Document { get; set; }

    public Tag Tag { get; set; }
}
