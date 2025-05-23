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
        [Authorize(policy: "EmailUser")]
        public int Get1()
        {
            return 500;
        }


        [HttpGet("admin")]
        [Authorize(Roles = "admin")]
        public int Get3()
        {
            return 5000;
        }
    }
}
