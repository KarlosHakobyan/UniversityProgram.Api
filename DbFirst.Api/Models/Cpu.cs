using System;
using System.Collections.Generic;

namespace DbFirst.Api.Models;

public partial class Cpu
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int LaptopId { get; set; }

    public virtual Laptop Laptop { get; set; } = null!;
}
