using System.ComponentModel.DataAnnotations;

namespace UniversityProgram.Api.Models.CPU
{
    public class CpuUpdateModel
    {
        [Required(ErrorMessage = "Name Required")]
        [MinLength(2, ErrorMessage = "Name must be at least 2 characters long")]
        public string? Name { get; set; }
    }
}
