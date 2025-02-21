using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using UniversityProgram.BLL.Models.University;
using UniversityProgram.Data;
using UniversityProgram.Data.Entities;

namespace UniversityProgram.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UniversityController : ControllerBase
    {
        private readonly StudentDbContext _ctx;
        public UniversityController(StudentDbContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet()]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var university = await _ctx.Universities.ToListAsync(cancellationToken);
            return Ok(university);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] int Id, CancellationToken cancellationToken)
        {
            var university = await _ctx.Universities.FirstOrDefaultAsync(e => e.Id == Id, cancellationToken);
            if (university == null)
            {
                return NotFound();
            }
            return Ok(university);
        }

        [HttpPost()]
        public async Task<IActionResult> AddUni([FromBody] UniversityAddModel model, CancellationToken cancellationToken)
        {
            var university = new University
            {
                Name = model.Name
            };
            _ctx.Universities.Add(university);
            await _ctx.SaveChangesAsync(cancellationToken);
            return Ok();
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Update([FromRoute] int Id, [FromBody] UniversityUpdateModel model, CancellationToken cancellationToken)
        {
            var university = await _ctx.Universities.FirstOrDefaultAsync(e => e.Id == Id, cancellationToken);
            if (university == null)
            {
                return NotFound();
            }
            university.Name = model.Name;
            _ctx.Universities.Update(university);
            await _ctx.SaveChangesAsync(cancellationToken);
            return Ok();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] int Id, CancellationToken cancellationToken)
        {
            var university = await _ctx.Universities.FirstOrDefaultAsync(e => e.Id == Id, cancellationToken);
            if (university == null)
            {
                return NotFound();
            }
            _ctx.Universities.Remove(university);
            await _ctx.SaveChangesAsync(cancellationToken);
            return Ok();
        }
    }
}
