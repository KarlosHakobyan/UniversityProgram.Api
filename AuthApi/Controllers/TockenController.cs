using Microsoft.AspNetCore.Mvc;

namespace AuthApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TockenController : ControllerBase
    {
        [HttpGet("login")]
        public string Login([FromServices] TockenGenerator gtr)
        {
            return gtr.Generate("karlos@gmail.com","admin");
        }
    }
}
