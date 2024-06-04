using System.ComponentModel.DataAnnotations;

namespace EducalBackend.ViewModels.Categories
{
    public class CategoryEditVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
