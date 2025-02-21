using System.ComponentModel.DataAnnotations;

namespace UniversityProgram.BLL.Models.Course
{
    public class CourseModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name Required")]
        [MinLength(2, ErrorMessage = "Name must be at least 2 characters long")]
        public string Name { get; set; } = default!;
        [Range(1000, 8000, ErrorMessage = "Insert value between 1000-8000")]
        public decimal Fee { get; set; }
        public string Paid { get; set; }
    }
}
