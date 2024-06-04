using EducalBackend.Models;

namespace EducalBackend.ViewModels
{
    public class HomeVM
    {        
        public IEnumerable<Course> Courses { get; set; }
        public IEnumerable<Category> Categories { get; set; }
       
    }
}
