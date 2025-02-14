using UniversityProgram.Api.Models.Address;
using UniversityProgram.Api.Models.Laptop;

namespace UniversityProgram.Api.Models.Student
{
    public class StudentWithAddressAddModel { 
        public StudentModel Student { get; set; } = default!;
        public AddressModel Address { get; set; } = default!;
    }
}
