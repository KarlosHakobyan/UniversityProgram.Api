namespace UniversityProgram.Api.Models.Course
{
    public class CourseModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public decimal Fee { get; set; }
        public string Paid{ get; set; }
    }
}
