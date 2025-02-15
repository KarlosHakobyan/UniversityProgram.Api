﻿using System.ComponentModel.DataAnnotations;

namespace UniversityProgram.Api.Models.Laptop
{
    public class LaptopUpdateModel
    {
        [MinLength(2, ErrorMessage = "Name must be at least 2 characters long")]
        public string Name { get; set; } = default!;
    }
}
