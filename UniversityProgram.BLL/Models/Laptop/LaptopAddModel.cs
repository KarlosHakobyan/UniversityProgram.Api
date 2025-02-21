using System.ComponentModel.DataAnnotations;

namespace UniversityProgram.BLL.Models.Laptop
{
    public class LaptopAddModel
    {
        public string Name { get; set; }
        public int? StudentId { get; set; }
    }
}
