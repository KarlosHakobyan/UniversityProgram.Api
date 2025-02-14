using UniversityProgram.Api.Models.Address;
using UniversityProgram.Api.Models.Laptop;

namespace UniversityProgram.Api.Models.Student
{
    public class StudentWithAddressModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public AddressModel? Address{ get; set; } = default!;
    }
}
