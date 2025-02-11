using UniversityProgram.Api.Models.Laptop;

namespace UniversityProgram.Api.Models.Student
{
    public class StudentWithLaptopModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public LaptopWithCpuModel? Laptop { get; set; } = default!;
    }
}
