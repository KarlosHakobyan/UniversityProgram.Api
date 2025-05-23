using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace NewAuthTest.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IdentityTestController : ControllerBase
    {
        private readonly ILogger<IdentityTestController> _logger;

        public IdentityTestController(ILogger<IdentityTestController> logger)
        {
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task Login([FromBody] UserModel model, [FromServices] SignInManager<IdentityUser> mng)
        {
            var result = await mng.PasswordSignInAsync(model.UserName, model.Password, false, false);

        }

    }
    public class UserModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
