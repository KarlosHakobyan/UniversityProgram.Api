namespace UniversityProgram.Api.Entities
{
    public class Cpu
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public int LaptopId { get; set; }
        public Laptop Laptop { get; set; } = default!; 
    }
}
