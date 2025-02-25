using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using AutoMapper;
using UniversityProgram.BLL.Models.Laptop;
using UniversityProgram.BLL.Models.CPU;
using UniversityProgram.Data;
using UniversityProgram.Domain.Entities;

namespace UniversityProgram.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LaptopController : ControllerBase
    {
        private readonly StudentDbContext _ctx;
        private readonly IMapper _mapper;

        public LaptopController(StudentDbContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            List<Laptop> laptops = await _ctx.Laptops.ToListAsync(cancellationToken);
            List<LaptopModel> models = _mapper.Map<List<LaptopModel>>(laptops);
            return Ok(models);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] LaptopAddModel model,[FromServices] IValidator<LaptopAddModel> validator, CancellationToken cancellationToken)
        {
            var result = await validator.ValidateAsync(model,cancellationToken);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }
            var laptop = _mapper.Map<Laptop>(model);
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

        [HttpGet("cpuName")]
        public async Task<IActionResult> GetLaptopWithCpuName(CancellationToken cancellationToken)
        {
            var laptops = await _ctx.Laptops
                .Include(e => e.Cpu)
                .ToListAsync(cancellationToken);
            var result = _mapper.Map<List <LaptopWithCpuNameModel >> (laptops);
            return Ok(result);
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
