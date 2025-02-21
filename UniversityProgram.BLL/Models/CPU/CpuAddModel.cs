using System.ComponentModel.DataAnnotations;

namespace UniversityProgram.BLL.Models.CPU
{
    public class CpuAddModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name Required")]
        [MinLength(2, ErrorMessage = "Name must be at least 2 characters long")]
        public string Name { get; set; } = default!;

    }
}
