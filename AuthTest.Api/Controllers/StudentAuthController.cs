using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthTest.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentAuthController : ControllerBase
    {
        private readonly ILogger<StudentAuthController> _logger;

        public StudentAuthController(ILogger<StudentAuthController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "StudentAuth")]
        [Authorize(policy:"Only Students")]
        public IActionResult Get()
        {
            return Ok("Student : GAGO");
        }


    }
}
