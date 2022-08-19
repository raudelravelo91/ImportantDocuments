using Microsoft.EntityFrameworkCore;
using ImportantDocuments;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ImportantDocuments.API.Domain;

[Index(nameof(Name), IsUnique = true)]
public class Tag: BaseModel
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(maximumLength: 25)]
    public string Name { get; set; }
    
    public List<Document> Documents { get; set; }
}
