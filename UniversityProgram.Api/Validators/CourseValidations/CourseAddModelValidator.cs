using FluentValidation;
using UniversityProgram.BLL.Models.Course;

namespace UniversityProgram.Api.Validators.CourseValidations
{
    public class CourseAddModelValidator : AbstractValidator<CourseAddModel>
    {
        public CourseAddModelValidator()
        {
            RuleFor(e=>e.Name).NotNull().WithMessage("Name Required")
                .MinimumLength(2).WithMessage("Name must be at least 2 characters long")
                    .MaximumLength(45).WithMessage("Name must be maximum 45 characters long");

            RuleFor(e => e.Fee).NotNull().WithMessage("Insert value between 1000-8000")
                .InclusiveBetween(1000, 8000).WithMessage("Insert value between 1000-8000");
        }
    }
}
