﻿namespace EducalBackend.ViewModels.Courses
{
    public class CourseEditVM
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Instructor { get; set; }
        public string? Duration { get; set; }
        public string? Price { get; set; }
        public int CategoryId { get; set; }
        public List<CourseImageVM>? Images { get; set; }

        public List<IFormFile>? NewImages { get; set; }
    }
}
