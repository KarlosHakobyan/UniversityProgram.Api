using System.ComponentModel.DataAnnotations;

namespace UniversityProgram.Api.Models.Address
{
    public class AddressModel
    {
        public int Id { get; set; }
        [MinLength(2, ErrorMessage = "Address must be at least 2 characters long")]
        public string Address { get; set; } = default!;
    }
}
