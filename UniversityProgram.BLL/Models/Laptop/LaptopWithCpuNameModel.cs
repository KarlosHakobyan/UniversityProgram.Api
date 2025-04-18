﻿using System.ComponentModel.DataAnnotations;

namespace UniversityProgram.BLL.Models.Laptop
{
    public class LaptopWithCpuNameModel
    {
        public int Id { get; set; }
        public string LaptopName { get; set; } = default!;
        public string CpuName { get; set; } = default!;

    }
}
