namespace UniversityProgram.Domain.Entities
{
    public class CourseStudent
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int StudentId { get; set; }
        public Course Course { get; set; } = default!;
        public StudentBase Student { get; set; } = default!;
        public bool Paid { get; set; }
    }
}
