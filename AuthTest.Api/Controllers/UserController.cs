using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AuthTest.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        [HttpGet("login")]
        public IActionResult Login()
        {
            var claim1 = new Claim("usertype", "student");
            var claim2 = new Claim("UrishRoleType", "admin");
            var claimIdentity = new ClaimsIdentity(
                new List<Claim>() { claim1, claim2 },
                "Cookie",
                ClaimsIdentity.DefaultNameClaimType,
                roleType: "UrishRoleType"
                );
            var claimsPrincipal = new ClaimsPrincipal(claimIdentity);
            return SignIn(claimsPrincipal, "Cookie");
        }

        [HttpDelete]
        [Authorize(Roles = "admin")]
        public IActionResult Delete()
        {
            return Ok("Student has Deleted.");  //return Ok("Successful Login from controller."); COOKIE CHI VERADARDZNUM ES DEPQUM U SXAL A LINUM.
        }

        [HttpGet("test")]
        [AllowAnonymous]
        public IActionResult Test()
        {
            return Ok("Test");
        }

       
    }
}