namespace StudentApiForMediator.Models
{
    public class StudentAddModel
    {   
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }

        public int BookId { get; set; }
        public int CourseId { get; set; }

    }
}
