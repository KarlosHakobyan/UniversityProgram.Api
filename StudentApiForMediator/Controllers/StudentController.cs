using Mediator;
using Microsoft.AspNetCore.Mvc;
using StudentApiForMediator.Data;
using StudentApiForMediator.Models;
using StudentApiForMediator.Requests;

namespace StudentApiForMediator.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class StudentController : ControllerBase
    {
        private readonly IMediator _mediator;
        public StudentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] StudentAddModel model)
        {
            var request = new StudentAddRequest()
            {
                Id = model.Id,
                Name = model.Name,
                Email = model.Email
            };
            var response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpGet]
        public void Test([FromServices] Database dtb)
        { 
            var books = dtb.Books.ToList();
            var students = dtb.Students.ToList();
            var courses = dtb.Courses.ToList();
            Console.WriteLine();
        }
    }
}
