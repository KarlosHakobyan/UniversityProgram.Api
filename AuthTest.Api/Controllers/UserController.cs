using AuthTest.Api.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Text;

namespace AuthTest.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserDbContext _ctx;

        public UserController(UserDbContext ctx)
        {
            _ctx = ctx;
        }

        [HttpPost]
        public IActionResult Register([FromBody] UserModel model)
        {
            var user = new User
            {
                Email = model.Email,
                PasswordHash = HashPassword(model.Password)
            };
            _ctx.Users.Add(user);
            _ctx.SaveChanges();
            return Ok("User has been registered.");
        }


        public static string HashPassword(string password)
        {   
            using var sha256 = System.Security.Cryptography.SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            var hashedPassword = Convert.ToBase64String(hashedBytes);
            return hashedPassword; 
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody]UserModel model)
        {   var user = _ctx.Users.FirstOrDefault(u => u.Email == model.Email);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var passwordHash = HashPassword(model.Password);

            if (user.PasswordHash != passwordHash)
            {
                return Unauthorized("Invalid password.");
            }

            var role = user.Role ?? "user";
            var claim1 = new Claim("usertype", "student");
            var claim2 = new Claim("UrishRoleType", role);
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

    public class UserModel
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}