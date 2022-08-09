using System.ComponentModel.DataAnnotations;

namespace ImportantDocuments.Domain
{
    public class Document
    {
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength: 200)]
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
