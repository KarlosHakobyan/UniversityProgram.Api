namespace UniversityProgram.Api.Entities
{
    public class University
    {   
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public IEnumerable<Student> Students { get; set; } = new List<Student>();
    }
}
