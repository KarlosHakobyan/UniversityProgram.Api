using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using UniversityProgram.Api.Entities;
using UniversityProgram.Api.Models.Course;
using UniversityProgram.Api.Models.CPU;
using UniversityProgram.Api.Repositories;
using UniversityProgram.Api.Repositories.CourseRep;

namespace UniversityProgram.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseRepository _repository;

        public CourseController(ICourseRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken token)
        {
            return Ok(await _repository.GetCourses(token));
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] int Id,CancellationToken token)
        {
            var course = await _repository.GetCourseByID(Id,token);
            if (course == null)
            {
                return NotFound();
            }
            return Ok(course);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody]CourseAddModel model,[FromServices] IValidator<CourseAddModel> validator,CancellationToken cancellationToken)
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
            await _repository.AddCourse(course,cancellationToken);
            
            return Ok();
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateCourseById([FromRoute] int Id, [FromBody] CourseUpdateModel model, CancellationToken cancellationToken)
        {
            bool isUpdated = await _repository.UpdateCourseById(Id, model, cancellationToken);
            if (!isUpdated)
            {
                return NotFound();
            }

            return Ok("Course updated successfully");
        }

        [HttpPut("{Id}/Fee")]
        public async Task<IActionResult> UpdateFee(
            [FromRoute] int Id,
            [FromQuery] decimal fee,
            [FromServices] IValidator<Course> validator,
            CancellationToken cancellationToken)
        {
            bool isUpdated = await _repository.UpdateFeeById(Id, fee, validator, cancellationToken);

            if (!isUpdated)
            {
                return BadRequest("Invalid inserted fee value [from 1000 to 8000] or course not found");
            }

            return Ok("Course fee updated successfully");
        }



        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] int Id, CancellationToken cancellationToken)
        {
            bool isDeleted=await _repository.DeleteCourseById(Id,cancellationToken);
            if (!isDeleted)
            {
                return BadRequest("Course not found");
            }

            return Ok("Course deleted");
        }

    }
}
