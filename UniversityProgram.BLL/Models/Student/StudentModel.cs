using System.ComponentModel.DataAnnotations;
using UniversityProgram.BLL.Models.Laptop;

namespace UniversityProgram.BLL.Models.Student
{
    public class StudentModel
    {
        public int Id { get; set; }
        [MinLength(2, ErrorMessage = "Name must be at least 2 characters long")]
        public string Name { get; set; } = default!;
        [EmailAddress(ErrorMessage = "Input correct Email address")]
        [MinLength(8, ErrorMessage = "Email must be at least 8 characters long")]
        public string Email { get; set; } = default!;
        public decimal Money { get; set; }

        public LaptopModel Laptop { get; set; } = default!;

    }
}
