using System;
using System.Collections.Generic;

namespace DbFirst.Api.Models;

public partial class Library
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
