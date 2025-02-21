using System.ComponentModel.DataAnnotations;

namespace UniversityProgram.BLL.Models.Laptop
{
    public class LaptopModel
    {
        public int Id { get; set; }

        [MinLength(2, ErrorMessage = "Name must be at least 2 characters long")]
        public string Name { get; set; } = default!;

    }
}
