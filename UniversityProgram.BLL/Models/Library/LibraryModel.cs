using System.ComponentModel.DataAnnotations;

namespace UniversityProgram.BLL.Models.Library
{
    public class LibraryModel
    {
        public int Id { get; }

        [MinLength(2, ErrorMessage = "Name must be at least 2 characters long")]

        public string Name { get; set; } = default!;
    }
}
