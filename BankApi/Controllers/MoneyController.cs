using Microsoft.AspNetCore.Mvc;

namespace BankApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MoneyController : ControllerBase
    {

        public int Get()
        {
            return 50;
        }
    }
}
