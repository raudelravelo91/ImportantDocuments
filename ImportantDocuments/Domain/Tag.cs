using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ImportantDocuments.API.Domain
{
    [Index(nameof(Name), IsUnique = true)]
    public class Tag : BaseModel
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(25)]
        public string Name { get; set; }
        
        public List<Document> Documents { get; set; }
    }
}
