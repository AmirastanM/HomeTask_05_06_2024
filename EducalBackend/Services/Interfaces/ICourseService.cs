using EducalBackend.Models;
using EducalBackend.ViewModels.Courses;

namespace EducalBackend.Services.Interfaces
{
    public interface ICourseService
    {
        Task<IEnumerable<Course>> GetAllWithImagesAsync();
        Task<IEnumerable<Course>> GetAllAsync();
        Task<Course> GetByIdWithAllDatasAsync(int id);
        Task<Course> GetByIdAsync(int id);        
        IEnumerable<CourseVM> GetMappedDatas(IEnumerable<Course> courses);       
        Task CreateAsync(Course course);
        Task DeleteAsync(Course course);
        Task EditAsync();
        Task<CourseImage> GetCourseImageByIdAsync(int id);
        Task<Course> GetCourseByNameAsync(string name);
        Task ImageDeleteAsync(CourseImage image);
    }
}
