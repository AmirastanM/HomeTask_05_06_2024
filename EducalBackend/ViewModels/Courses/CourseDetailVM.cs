namespace EducalBackend.ViewModels.Courses
{
    public class CourseDetailVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Instructor { get; set; }
        public string Duration { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public List<CourseImageVM> Images { get; set; }
    }
}
