using UniversityProgram.Api.Models.Laptop;

namespace UniversityProgram.Api.Models.Student
{
    public class StudentWithLaptopAddModel
    {     
        public StudentModel Student{ get; set; } = default!;
        public LaptopModel Laptop { get; set; } = default!;
    }
}
