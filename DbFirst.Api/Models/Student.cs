using System;
using System.Collections.Generic;

namespace DbFirst.Api.Models;

public partial class Student
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int? LibraryId { get; set; }

    public decimal Money { get; set; }

    public virtual ICollection<CourseStudent> CourseStudents { get; set; } = new List<CourseStudent>();

    public virtual Laptop? Laptop { get; set; }

    public virtual Library? Library { get; set; }

    public virtual ICollection<University> Universities { get; set; } = new List<University>();
}
