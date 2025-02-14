using UniversityProgram.Api.Models.Laptop;

namespace UniversityProgram.Api.Models.Student
{
    public class StudentModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public decimal Money { get; set; }

        public LaptopModel Laptop { get; set; } = default!;
    }
}
