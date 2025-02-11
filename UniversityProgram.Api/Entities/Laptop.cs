namespace UniversityProgram.Api.Entities
{
    public class Laptop
    {
        public int Id{ get; set; }
        public string Name { get; set; } = default!;
        public int StudentId { get; set; }
        public Student Student { get; set; } = default!;
        public Cpu? Cpu { get; set; }
    }
}
