using System.ComponentModel.DataAnnotations;

namespace EducalBackend.ViewModels.Categories
{
    public class CategoryCreateVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
