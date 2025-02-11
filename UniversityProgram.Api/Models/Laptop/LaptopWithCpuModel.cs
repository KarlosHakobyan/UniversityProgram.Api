using UniversityProgram.Api.Models.CPU;

namespace UniversityProgram.Api.Models.Laptop
{
    public class LaptopWithCpuModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;

        public CpuModel? Cpu { get; set; } = default!;
    }
}
