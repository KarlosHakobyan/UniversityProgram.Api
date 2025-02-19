using System.ComponentModel.DataAnnotations;

namespace UniversityProgram.Api.Models.Course
{
    public class CourseAddModel
    {
        public string Name { get; set; }
        public decimal Fee { get; set; }
    }
}
