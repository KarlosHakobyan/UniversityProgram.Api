namespace StudentApiForMediator.Models
{
    public class StudentAddModel
    {   
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;

        public int BookId { get; set; }
        public int CourseId { get; set; }

    }
}
