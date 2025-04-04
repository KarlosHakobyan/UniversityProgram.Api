﻿using System.ComponentModel.DataAnnotations;

namespace UniversityProgram.Mvc.Models
{
    public class UserViewModel
    {
        public string Name { get; set; } = default!;
        public int Age { get; set; }
        public string ImageUrl { get; set; } = default!;
        [EmailAddress]
        public string Email { get; set; }
    }
}
