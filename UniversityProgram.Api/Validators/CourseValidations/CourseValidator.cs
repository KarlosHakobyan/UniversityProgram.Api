using FluentValidation;
using UniversityProgram.Domain.Entities;

namespace UniversityProgram.Api.Validators.CourseValidations
{
    public class CourseValidator : AbstractValidator<Course>
    {
        public CourseValidator()
        {
            RuleFor(e => e.Fee).NotNull().WithMessage("Insert value between 1000-8000")
                .InclusiveBetween(1000, 8000).WithMessage("Insert value between 1000-8000");
        }
    }
}
