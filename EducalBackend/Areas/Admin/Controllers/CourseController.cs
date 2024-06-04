using EducalBackend.Controllers;
using EducalBackend.Helpers;
using EducalBackend.Models;
using EducalBackend.Services.Interfaces;
using EducalBackend.ViewModels.Courses;
using Microsoft.AspNetCore.Mvc;

namespace EducalBackend.Areas.Admin.Controllers
{

    [Area("admin")]
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly ICategoryService _categoryService;
        private readonly IWebHostEnvironment _env;
        public CourseController(ICourseService courseService, ICategoryService categoryService, IWebHostEnvironment env)
        {
            _courseService = courseService;
            _categoryService = categoryService;
            _env = env;
        }

        [HttpGet]        
            public async Task<IActionResult> Index()
            {
                IEnumerable<Course> courses = await _courseService.GetAllWithImagesAsync();
            List<CourseVM> courseVMs = new();
            foreach (var item in courses)
            {
                courseVMs.Add(new CourseVM { CategoryName = item.Category.Name, Description = item.Description, Duration = item.Duration, Instructor = item.Instructor, MainImage = item.CourseImages.FirstOrDefault(m=>m.IsMain = true).Name});
            }
;                return View(courseVMs);
            }
        



        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();
            var existProduct = await _courseService.GetByIdWithAllDatasAsync((int)id);
            if (existProduct is null) return NotFound();

            List<CourseImageVM> images = new();
            foreach (var item in existProduct.CourseImages)
            {
                images.Add(new CourseImageVM
                {
                    Image = item.Name,
                    IsMain = item.IsMain

                });
            }
            CourseDetailVM response = new()
            {
                Name = existProduct.Name,
                Description = existProduct.Description,
                Category = existProduct.Category.Name,
                Price = existProduct.Price,
                Images = images
            };
            return View(response);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.categories = await _categoryService.GetAllSelectedAsync();
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseCreateVM request)
        {
            ViewBag.categories = await _categoryService.GetAllSelectedAsync();
            if (!ModelState.IsValid)
            {
                return View();

            }

            foreach (var item in request.Images)
            {
                if (!item.CheckFileSize(500))
                {
                    ModelState.AddModelError("Images", "Image size must be max 500 KB");
                    return View();
                }

                if (!item.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Images", "File type must be only image");

                    return View();
                }
            }
            List<CourseImage> images = new();
            foreach (var item in request.Images)
            {
                string fileName = $"{Guid.NewGuid()}-{item.FileName}";
                string path = _env.GenerateFilePath("img", fileName);
                await item.SaveFileToLocalAsync(path);
                images.Add(new CourseImage { Name = fileName });
            }

            images.FirstOrDefault().IsMain = true;
            Course course = new()
            {
                Name = request.Name,
                Description = request.Description,
                CategoryId = request.CategoryId,
                Price = decimal.Parse(request.Price.Replace(".", ",")),
                CourseImages = images

            };

            await _courseService.CreateAsync(course);


            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();
            var existProduct = await _courseService.GetByIdWithAllDatasAsync((int)id);
            if (existProduct is null) return NotFound();

            foreach (var item in existProduct.CourseImages)
            {
                string path = _env.GenerateFilePath("img", item.Name);

                path.DeleteFileFromLocal();
            }
            await _courseService.DeleteAsync(existProduct);
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {


            if (id is null) return BadRequest();

            var existProduct = await _courseService.GetByIdWithAllDatasAsync((int)id);

            if (existProduct is null) return NotFound();



            ViewBag.categories = await _categoryService.GetAllSelectedAsync();

            List<CourseImageVM> images = new();

            foreach (var item in existProduct.CourseImages)
            {
                images.Add(new CourseImageVM
                {
                    Id = item.Id,
                    Image = item.Name,
                    IsMain = item.IsMain
                });
            }

            CourseEditVM response = new()
            {
                Name = existProduct.Name,
                Description = existProduct.Description,
                Price = existProduct.Price.ToString().Replace(",", "."),
                Images = images
            };





            return View(response);
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, CourseEditVM request)
        {
            ViewBag.categories = await _categoryService.GetAllSelectedAsync();
            if (!ModelState.IsValid)
            {
                var product = await _courseService.GetByIdWithAllDatasAsync((int)id);

                List<CourseImageVM> Images = new();

                foreach (var item in product.CourseImages)
                {
                    Images.Add(new CourseImageVM
                    {
                        Image = item.Name,
                        IsMain = item.IsMain
                    });
                }

                return View(new CourseEditVM { Images = Images });

            }

            if (id == null) return BadRequest();
            var products = await _courseService.GetByIdWithAllDatasAsync((int)id);
            if (products == null) return NotFound();


           

            List<CourseImage> images = new();
            foreach (var item in request.NewImages)
            {
                string fileName = $"{Guid.NewGuid()}-{item.FileName}";
                string path = _env.GenerateFilePath("img", fileName);
                await item.SaveFileToLocalAsync(path);
                images.Add(new CourseImage { Name = fileName });
            }


            foreach (var item in request.NewImages)
            {


                if (!item.CheckFileType("image/"))
                {
                    ModelState.AddModelError("NewImages", "Input can accept only image format");
                    products.CourseImages = images;
                    return View(request);

                }
                if (!item.CheckFileSize(500))
                {
                    ModelState.AddModelError("NewImages", "Image size must be max 500 KB ");
                    products.CourseImages = images;
                    return View(request);
                }


            }

            foreach (var item in request.NewImages)
            {
                string oldPath = _env.GenerateFilePath("img", item.Name);
                oldPath.DeleteFileFromLocal();
                string fileName = Guid.NewGuid().ToString() + "-" + item.FileName;
                string newPath = _env.GenerateFilePath("img", fileName);

                await item.SaveFileToLocalAsync(newPath);


                if (request.Name is not null)
                {
                    products.Name = request.Name;
                }
                if (request.Description is not null)
                {
                    products.Description = request.Description;
                }


                products.CourseImages.Add(new CourseImage { Name = fileName });

            }


            await _courseService.EditAsync();
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> isMain(int? id)
        {
            if (id is null) return BadRequest();
            var productImage = await _courseService.GetCourseImageByIdAsync((int)id);

            if (productImage is null) return NotFound();

           
            var productID = productImage.CourseId;

            var pro = await _courseService.GetByIdWithAllDatasAsync(productID);
            foreach (var item in pro.CourseImages)
            {
                item.IsMain = false;
            }
            productImage.IsMain = true;



            await _courseService.EditAsync();
            return RedirectToAction(nameof(Index));

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> imageDelete(int? id)
        {
            if (id is null) return BadRequest();
            var productImage = await _courseService.GetCourseImageByIdAsync((int)id);

            if (productImage is null) return NotFound();

           


            string path = _env.GenerateFilePath("img", productImage.Name);

            path.DeleteFileFromLocal();


            await _courseService.ImageDeleteAsync(productImage);
            return RedirectToAction(nameof(Index));

        }
    }
}
