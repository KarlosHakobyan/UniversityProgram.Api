using System;
using System.Collections.Generic;

namespace DbFirst.Api.Models;

public partial class Course
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal Fee { get; set; }

    public virtual ICollection<CourseStudent> CourseStudents { get; set; } = new List<CourseStudent>();
}
