using Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
        [HttpGet("Open")]

        public IActionResult Open()
        {
            var model = new StudentModel
            {
                Name = "Open Student",
                Id = 5
            };
            return Ok(model);
        }

        [HttpGet("Private")]
        [Authorize]
        public IActionResult Private()
        {
            var model = new StudentModel
            {
                Name = "Private Student",
                Id = 7
            };
            return Ok(model);
        }

        [HttpGet]
        public IActionResult Get([FromQuery] int id)
        {
            var model = new StudentModel
            {
                Name = "Student ",
                Id = id
            };
            return Ok(model);
        }

        [HttpDelete]
        public IActionResult Delete([FromRoute] int id)
        {
            var model = new StudentModel
            {
                Name = "Student ",
                Id = id
            };
            return Ok(model);
        }

        [HttpPost]
        public IActionResult Add([FromBody] StudentModel model,CancellationToken tkn)
        {
          return Ok(model);
        }
    }
}
