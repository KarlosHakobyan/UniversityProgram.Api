using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityProgram.Api.Entities;
using UniversityProgram.Api.Models.CPU;
using UniversityProgram.Api.Models.Library;
using System.Threading;
using System.Threading.Tasks;

namespace UniversityProgram.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CpuController : ControllerBase
    {
        private readonly StudentDbContext _ctx;
        public CpuController(StudentDbContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var cpu = await _ctx.Cpu.ToListAsync(cancellationToken);
            return Ok(cpu);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] int Id, CancellationToken cancellationToken)
        {
            var cpu = await _ctx.Cpu.FirstOrDefaultAsync(e => e.Id == Id, cancellationToken);
            if (cpu == null)
            {
                return NotFound();
            }
            return Ok(cpu);
        }

        [HttpPost]
        public async Task<IActionResult> AddCpu([FromBody] CpuAddModel model, CancellationToken cancellationToken)
        {
            var cpu = new Cpu
            {
                Name = model.Name
            };
            _ctx.Cpu.Add(cpu);
            await _ctx.SaveChangesAsync(cancellationToken);
            return Ok();
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Update([FromRoute] int Id, [FromBody] CpuUpdateModel model, CancellationToken cancellationToken)
        {
            var cpu = await _ctx.Cpu.FirstOrDefaultAsync(e => e.Id == Id, cancellationToken);
            if (cpu == null)
            {
                return NotFound();
            }
            cpu.Name = model.Name;
            _ctx.Cpu.Update(cpu);
            await _ctx.SaveChangesAsync(cancellationToken);
            return Ok();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] int Id, CancellationToken cancellationToken)
        {
            var cpu = await _ctx.Cpu.FirstOrDefaultAsync(e => e.Id == Id, cancellationToken);
            if (cpu == null)
            {
                return NotFound();
            }
            _ctx.Cpu.Remove(cpu);
            await _ctx.SaveChangesAsync(cancellationToken);
            return Ok();
        }
    }
}
