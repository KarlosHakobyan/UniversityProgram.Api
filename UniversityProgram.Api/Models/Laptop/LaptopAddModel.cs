using System.ComponentModel.DataAnnotations;

namespace UniversityProgram.Api.Models.Laptop
{
    public class LaptopAddModel
    {              
        public string Name { get; set; }
        public int? StudentId { get; set; }
    }
}
