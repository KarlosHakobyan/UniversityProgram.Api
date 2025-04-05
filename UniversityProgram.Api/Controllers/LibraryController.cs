using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using UniversityProgram.BLL.Models.Library;
using UniversityProgram.Data;
using UniversityProgram.Domain.Entities;

namespace UniversityProgram.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LibraryController : ControllerBase
    {
        private readonly StudentDbContext _ctx;
        // private readonly IMapper _mapper;

        public LibraryController(StudentDbContext ctx) // , IMapper mapper)
        {
            _ctx = ctx;
            // _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var library = await _ctx.Libraries.ToListAsync(cancellationToken);
            return Ok(library);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] int Id, CancellationToken cancellationToken)
        {
            var library = await _ctx.Libraries.FirstOrDefaultAsync(e => e.Id == Id, cancellationToken);
            if (library == null)
            {
                return NotFound();
            }
            return Ok(library);
        }

        [HttpPost]
        public async Task<IActionResult> AddLib([FromBody] LibraryAddModel model, CancellationToken cancellationToken)
        {
            var library = new Library
            {
                Name = model.Name
            };
            _ctx.Libraries.Add(library);
            await _ctx.SaveChangesAsync(cancellationToken);
            return Ok();
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Update([FromRoute] int Id, [FromBody] LibraryUpdateModel model, CancellationToken cancellationToken)
        {
            var library = await _ctx.Libraries.FirstOrDefaultAsync(e => e.Id == Id, cancellationToken);
            if (library == null)
            {
                return NotFound();
            }
            library.Name = model.Name;
            _ctx.Libraries.Update(library);
            await _ctx.SaveChangesAsync(cancellationToken);
            return Ok();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] int Id, CancellationToken cancellationToken)
        {
            var library = await _ctx.Libraries.FirstOrDefaultAsync(e => e.Id == Id, cancellationToken);
            if (library == null)
            {
                return NotFound();
            }
            _ctx.Libraries.Remove(library);
            await _ctx.SaveChangesAsync(cancellationToken);
            return Ok();
        }
    }
}
