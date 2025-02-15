using System.ComponentModel.DataAnnotations;

namespace UniversityProgram.Api.Models.Address
{
    public class AddressAddModel
    {
        [MinLength(2, ErrorMessage = "Address must be at least 2 characters long")]
        public string Address { get; set; } = default!;
        public int? StudentId { get; set; }
    }
}
