using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using UniversityProgram.BLL.Models.Address;
using UniversityProgram.Data;
using UniversityProgram.Data.Entities;

namespace UniversityProgram.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AddressController : ControllerBase
    {
        private readonly StudentDbContext _ctx;

        public AddressController(StudentDbContext ctx)
        {
            _ctx = ctx;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddressAddModel model, CancellationToken cancellationToken)
        {
            var address = new AddressBase
            {
                Address = model.Address,
                StudentId = model.StudentId ?? 0
            };
            _ctx.Address.Add(address);
            await _ctx.SaveChangesAsync(cancellationToken);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var address = await _ctx.Address.ToListAsync(cancellationToken);
            return Ok(address);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] int Id, CancellationToken cancellationToken)
        {
            var address = await _ctx.Address.FirstOrDefaultAsync(e => e.Id == Id, cancellationToken);
            if (address == null)
            {
                return NotFound();
            }
            return Ok(address);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Update([FromRoute] int Id, [FromBody] AddressUpdateModel model, CancellationToken cancellationToken)
        {
            var address = await _ctx.Address.FirstOrDefaultAsync(e => e.Id == Id, cancellationToken);
            if (address == null)
            {
                return NotFound();
            }
            address.Address = model.Address;
            _ctx.Address.Update(address);
            await _ctx.SaveChangesAsync(cancellationToken);
            return Ok();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] int Id, CancellationToken cancellationToken)
        {
            var address = await _ctx.Address.FirstOrDefaultAsync(e => e.Id == Id, cancellationToken);
            if (address == null)
            {
                return NotFound();
            }
            _ctx.Address.Remove(address);
            await _ctx.SaveChangesAsync(cancellationToken);
            return Ok();
        }
        




    }
}
