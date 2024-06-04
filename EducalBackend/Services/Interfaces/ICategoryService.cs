using EducalBackend.Models;
using EducalBackend.ViewModels.Categories;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EducalBackend.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<IEnumerable<CategoryCourseVM>> GetAllWithCourseAsync();
        Task<Category> GetByIdAsync(int id);
        Task<bool> ExistAsync(string name);
        Task CreateAsync(Category category);
        Task DeleteAsync(Category category);
        Task<bool> ExistExceptByIdAsync(int id, string name);            
        IEnumerable<CategoryCourseVM> GetMappedDatas(IEnumerable<Category> category);              
        Task<SelectList> GetAllSelectedAsync();
    }
}
