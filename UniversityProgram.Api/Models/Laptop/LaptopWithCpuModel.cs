﻿using System.ComponentModel.DataAnnotations;
using UniversityProgram.Api.Models.CPU;

namespace UniversityProgram.Api.Models.Laptop
{
    public class LaptopWithCpuModel
    {
        public int Id { get; set; }
        [MinLength(2, ErrorMessage = "Name must be at least 2 characters long")]
        public string Name { get; set; } = default!;

        public CpuModel? Cpu { get; set; } = default!;
    }
}
