using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using UniversityProgram.Api.Entities;
using UniversityProgram.Api.Models.Course;
using UniversityProgram.Api.Models.CPU;

namespace UniversityProgram.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly StudentDbContext ctx;

        public CourseController(StudentDbContext ctx)
        {
            this.ctx= ctx;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            return Ok(await ctx.Courses.ToListAsync(cancellationToken));
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] int Id, CancellationToken cancellationToken)
        {
            var course = await ctx.Courses.FirstOrDefaultAsync(e => e.Id == Id, cancellationToken);
            if (course == null)
            {
                return NotFound();
            }
            return Ok(course);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody]CourseAddModel model,CancellationToken cancellationToken)
        {
            var course = new Course()
            {
                Name = model.Name
            };
            ctx.Courses.Add(course);
            await ctx.SaveChangesAsync(cancellationToken);
            return Ok();
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Update([FromRoute] int Id, [FromBody] CourseUpdateModel model, CancellationToken cancellationToken)
        {
            var course = await ctx.Courses.FirstOrDefaultAsync(e => e.Id == Id, cancellationToken);
            if (course == null)
            {
                return NotFound();
            }
            course.Name = model.Name;
            ctx.Courses.Update(course);
            await ctx.SaveChangesAsync(cancellationToken);
            return Ok();
        }

        [HttpPut("{Id}/Fee")]
        public async Task<IActionResult> UpdateFee([FromRoute] int Id, [FromQuery] decimal fee, CancellationToken cancellationToken)
        {
            var course = await ctx.Courses.FirstOrDefaultAsync(e => e.Id == Id, cancellationToken);
            if (course == null)
            {
                return NotFound();
            }
            course.Fee = fee;
            ctx.Courses.Update(course);
            await ctx.SaveChangesAsync(cancellationToken);
            return Ok();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] int Id, CancellationToken cancellationToken)
        {
            var course = await ctx.Courses.FirstOrDefaultAsync(e => e.Id == Id, cancellationToken);
            if (course == null)
            {
                return NotFound();
            }
            ctx.Courses.Remove(course);
            await ctx.SaveChangesAsync(cancellationToken);
            return Ok();
        }             

    }
}
