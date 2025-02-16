using FluentValidation;
using UniversityProgram.Api.Entities;

namespace UniversityProgram.Api.Validators.StudentValidations
{
    public class StudentBaseValidator : AbstractValidator<StudentBase>
    {
        public StudentBaseValidator()
        {
            RuleFor(e => e.Money).NotNull().WithMessage("Insert value between 100-10000")
                .InclusiveBetween(100, 10000).WithMessage("Insert value between 100-10000");
            
        }
    }
}
