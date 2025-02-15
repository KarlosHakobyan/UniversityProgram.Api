using FluentValidation;
using UniversityProgram.Api.Models.Laptop;

namespace UniversityProgram.Api.Validators.LaptopValidations
{
    public class LaptopAddModelValidator : AbstractValidator<LaptopAddModel>
    {
        public LaptopAddModelValidator()
        {
            RuleFor(e => e.Name).NotNull().WithMessage("Name Required")
                .MinimumLength(2).WithMessage("Name must be at least 2 characters long")
                    .MaximumLength(45).WithMessage("Name must be maximum 45 characters long");
        }
    }
}
