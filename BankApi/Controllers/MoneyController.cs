using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MoneyController : ControllerBase
    {
        [HttpGet]
        public int Get()
        {
            return 50;
        }

        [HttpGet("get1")]
        [Authorize]
        public int Get1()
        {
            return 500;
        }
    }
}
