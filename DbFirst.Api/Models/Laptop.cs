using System;
using System.Collections.Generic;

namespace DbFirst.Api.Models;

public partial class Laptop
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int StudentId { get; set; }

    public virtual Cpu? Cpu { get; set; }

    public virtual Student Student { get; set; } = null!;
}
