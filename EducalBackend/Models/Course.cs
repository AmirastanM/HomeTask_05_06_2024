using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EducalBackend.Models
{
    public class Course : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; } 
        public string Instructor { get; set; }
        public string Duration { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public List<CourseImage> CourseImages { get; set; }


    }
}
