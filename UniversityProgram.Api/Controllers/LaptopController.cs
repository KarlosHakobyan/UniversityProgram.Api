using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityProgram.Api.Entities;
using UniversityProgram.Api.Models.Laptop;
using UniversityProgram.Api.Models.Student;
using UniversityProgram.Api.Models.CPU;
using UniversityProgram.Api.Models.Library;
using System.Threading;

namespace UniversityProgram.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LaptopController : ControllerBase
    {
        private readonly StudentDbContext _ctx;
        public LaptopController(StudentDbContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var laptops = await _ctx.Laptops.ToListAsync(cancellationToken);
            return Ok(laptops);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] LaptopAddModel model, CancellationToken cancellationToken)
        {
            var laptop = new Laptop
            {
                Name = model.Name,
                StudentId = model.StudentId ?? 0
            };
            _ctx.Laptops.Add(laptop);
            await _ctx.SaveChangesAsync(cancellationToken);
            return Ok();
        }

        [HttpPut("{Id}/Cpu")]
        public async Task<IActionResult> AddCpu([FromRoute] int Id, [FromBody] CpuAddModel model, CancellationToken cancellationToken)
        {
            var laptop = await _ctx.Laptops.FirstOrDefaultAsync(e => e.Id == Id, cancellationToken);
            if (laptop == null)
            {
                return NotFound();
            }
            var cpu = new Cpu
            {
                Name = model.Name,
                LaptopId = laptop.Id
            };

            laptop.Cpu = cpu;
            _ctx.Update(laptop);
            await _ctx.SaveChangesAsync(cancellationToken);
            return Ok();
        }

        [HttpGet("{Id}/Cpu")]
        public async Task<IActionResult> GetByIdWithLaptop([FromRoute] int Id, CancellationToken cancellationToken)
        {
            var laptop = await _ctx.Laptops.Include(e => e.Cpu)
                .FirstOrDefaultAsync(e => e.Id == Id, cancellationToken);

            if (laptop == null)
            {
                return NotFound();
            }

            var model = new LaptopWithCpuModel
            {
                Id = laptop.Id,
                Name = laptop.Name,
                Cpu = laptop.Cpu is null ? null
                       : new CpuModel
                       {
                           Id = laptop.Cpu.Id,
                           Name = laptop.Cpu.Name
                       }
            };
            return Ok(model);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Update([FromRoute] int Id, [FromBody] LaptopUpdateModel model, CancellationToken cancellationToken)
        {
            var laptop = await _ctx.Laptops.FirstOrDefaultAsync(e => e.Id == Id, cancellationToken);
            if (laptop == null)
            {
                return NotFound();
            }
            laptop.Name = model.Name;
            _ctx.Laptops.Update(laptop);
            await _ctx.SaveChangesAsync(cancellationToken);
            return Ok();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] int Id, CancellationToken cancellationToken)
        {
            var laptop = await _ctx.Laptops.FirstOrDefaultAsync(e => e.Id == Id, cancellationToken);
            if (laptop == null)
            {
                return NotFound();
            }
            _ctx.Laptops.Remove(laptop);
            await _ctx.SaveChangesAsync(cancellationToken);
            return Ok();
        }
    }
}
