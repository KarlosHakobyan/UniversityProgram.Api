using System.ComponentModel.DataAnnotations;

namespace UniversityProgram.BLL.Models.Course
{
    public class CourseAddModel
    {
        public string? Name { get; set; }
        public decimal Fee { get; set; }
    }
}
