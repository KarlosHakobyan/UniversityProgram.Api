using System.ComponentModel.DataAnnotations;

namespace UniversityProgram.Api.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        [Range (1000,8000,ErrorMessage ="Insert value between 1000-8000")]
        public decimal Fee { get; set; }
        public IEnumerable<CourseStudent> CourseStudents { get; set; } = new List<CourseStudent>();        
    }
}
