namespace UniversityProgram.Data.Entities
{
    public class AddressBase
    {
        public int Id { get; set; }
        public string Address { get; set; } = default!;
        public int StudentId { get; set; }
        public StudentBase Student { get; set; } = default!;
    }
}
