namespace UniversityProgram.Api.Models.Course
{
    public class CourseUpdateModel
    {
        public string Name { get; set; } = default!;
        public decimal Fee { get; set; }
    }
}
