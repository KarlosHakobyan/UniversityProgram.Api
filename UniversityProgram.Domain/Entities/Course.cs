using System.ComponentModel.DataAnnotations;

namespace UniversityProgram.Domain.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public decimal Fee { get; set; }
        public IEnumerable<CourseStudent> CourseStudents { get; set; } = new List<CourseStudent>();
    }
}
