using EducalBackend.Models;
using EducalBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EducalBackend.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public async Task<IActionResult> Index(int? id)
        {
            string hashData = Guid.NewGuid().ToString();
            ViewBag.Hash = hashData;
            if (id is null) return BadRequest();
            Course course = await _courseService.GetByIdWithAllDatasAsync((int)id);
            if (course is null) return NotFound();
            return View(course);
        }
    }
}
