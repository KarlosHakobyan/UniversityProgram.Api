using System.ComponentModel.DataAnnotations;

namespace UniversityProgram.BLL.Models.Student
{
    public class StudentUpdateModel
    {
        [EmailAddress(ErrorMessage = "Input correct Email address")]
        [MinLength(8, ErrorMessage = "Email must be at least 8 characters long")]
        public string Email { get; set; } = default!;
    }
}
