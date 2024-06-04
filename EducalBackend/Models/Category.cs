using System.ComponentModel.DataAnnotations;

namespace EducalBackend.Models
{
    public class Category : BaseEntity
    {        
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
}
