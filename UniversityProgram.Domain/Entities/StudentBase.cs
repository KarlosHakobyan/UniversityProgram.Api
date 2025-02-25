using System.ComponentModel.DataAnnotations;

namespace UniversityProgram.Domain.Entities
{
    public class StudentBase
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Address { get; set; } = default!;
        public Laptop? Laptop { get; set; } = default!;
        public int? LibraryId { get; set; }
        public decimal Money { get; set; }
        public uint Age { get; set; }
        public Library? Library { get; set; }
        public IEnumerable<University> Universities { get; set; } = new List<University>();
        public ICollection<CourseStudent> CourseStudents { get; set; } = new List<CourseStudent>();
    }
}

