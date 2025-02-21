using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using UniversityProgram.BLL.Models.Course;
using UniversityProgram.Data.Entities;
using UniversityProgram.Data.Repositories;

namespace UniversityProgram.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public CourseController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken token)
        {
            return Ok(await _uow.CourseRepository.GetCourses(token));
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] int Id, CancellationToken token)
        {
            var course = await _uow.CourseRepository.GetCourseByID(Id, token);
            if (course == null)
            {
                return NotFound();
            }
            return Ok(course);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CourseAddModel model, [FromServices] IValidator<CourseAddModel> validator, CancellationToken cancellationToken)
        {
            var result = await validator.ValidateAsync(model, cancellationToken);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            var course = new Course()
            {
                Name = model.Name,
                Fee = model.Fee
            };
            _uow.CourseRepository.AddCourse(course, cancellationToken);
            await _uow.Save(cancellationToken);
            return Ok();
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateCourseById([FromRoute] int Id, [FromBody] CourseUpdateModel model, CancellationToken cancellationToken)
        {
            var course = await _uow.CourseRepository.GetCourseByID(Id, cancellationToken);
            if (course == null)
            {
                return NotFound();
            }

            course.Name = model.Name;
            _uow.CourseRepository.UpdateCourse(course, cancellationToken);
            await _uow.Save(cancellationToken);
            return Ok();
        }


        [HttpPut("{Id}/Fee")]
        public async Task<IActionResult> UpdateFee(
        [FromRoute] int Id,
        [FromQuery] decimal fee,
        [FromServices] IValidator<Course> validator,
        CancellationToken cancellationToken)
        {
            var course = await _uow.CourseRepository.GetCourseByID(Id, cancellationToken);
            if (course == null)
            {
                return NotFound();
            }

            var validationResult = await validator.ValidateAsync(new Course { Fee = fee }, cancellationToken);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            course.Fee = fee;

            _uow.CourseRepository.UpdateCourse(course, cancellationToken);
            await _uow.Save(cancellationToken);
            return Ok();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] int Id, CancellationToken cancellationToken)
        {
            var course = await _uow.CourseRepository.GetCourseByID(Id, cancellationToken);
            if (course == null)
            {
                return NotFound();
            }
            _uow.CourseRepository.DeleteCourse(course,cancellationToken);
            await _uow.Save(cancellationToken);
            return Ok();
        }

    }
}
