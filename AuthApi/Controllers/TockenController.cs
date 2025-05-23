using Microsoft.AspNetCore.Mvc;

namespace AuthApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TockenController : ControllerBase
    {
        [HttpGet("login")]
        public IActionResult Login([FromServices] TokenGenerator gtr)
        {
            var token = gtr.Generate("karlos@gmail.com","admin");
            var accessToken = Save(token)
            return Redirect($"http://localhost:5072/User/login?token={accessToken}"); // anvtang dzev.
        }

        public string Save(string token)
        { 
            return Guid.NewGuid().ToString();
        }
    }
}
