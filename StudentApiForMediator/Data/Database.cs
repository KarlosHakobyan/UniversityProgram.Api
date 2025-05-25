namespace StudentApiForMediator.Data
{
    public class Database
    {
        public Database()
        {
            Books = new List<Book>()
            {
                new Book { Id = 1, Title = "C# Programming" },
                new Book { Id = 2, Title = "ASP.NET Core Development" },
                new Book { Id = 3, Title = "Entity Framework Core" },
                new Book { Id = 4, Title = "Design Patterns in C#" }
            };

            Courses = new List<Course>()
            {
                new Course { Id = 1, Name = "Introduction to C#" },
                new Course { Id = 2, Name = "Advanced ASP.NET Core" },
                new Course { Id = 3, Name = "Database Management with EF Core" },
                new Course { Id = 4, Name = "Software Design Patterns" }
            };
        }

        public ICollection<Student> Students { get; set; } = new List<Student>();
        public ICollection<Book> Books { get; set; } = new List<Book>();
        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }

    public class Student
    {
        public string Name { get; set; } = default!;
        public int Id { get; set; }
        public string Email { get; set; } = default!;
    }

    public class Book
    {
        public string Title { get; set; } = default!;
        public int Id { get; set; }

        public ICollection<Student> Students { get; set; } = new List<Student>();
    }

    public class Course
    {
        public string Name { get; set; } = default!;
        public int Id { get; set; }

        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}
