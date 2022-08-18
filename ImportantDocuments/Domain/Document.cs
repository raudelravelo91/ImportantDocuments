using System.ComponentModel.DataAnnotations;
using ImportantDocuments.API.Domain;

namespace ImportantDocuments.Domain
{
    public class Document : BaseModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public LocationType LocationType { get; set; }
        public List<Tag> Tags { get; set; }
    }

    public enum LocationType
    {
        None,
        PhysicalPath,
        URL
    }
}
