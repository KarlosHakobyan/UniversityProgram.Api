using System.ComponentModel.DataAnnotations;

namespace UniversityProgram.Api.Models.Course
{
    public class CourseAddModel
    {
        [Required(ErrorMessage = "Name Required")]
        [MinLength(2, ErrorMessage = "Name must be at least 2 characters long")]
        public string? Name { get; set; }
        [Range(1000, 8000, ErrorMessage = "Insert value between 1000-8000")]
        public decimal Fee { get; set; }
    }
}
