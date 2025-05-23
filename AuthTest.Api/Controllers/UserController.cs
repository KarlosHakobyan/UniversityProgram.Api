using AuthTest.Api.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Security.Cryptography;
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
            var salt = new byte[16]; // Generate a random salt.
            using (var rgn = RandomNumberGenerator.Create())
            {
                rgn.GetBytes(salt); //kecc patahakan kam psevdorandom tiv.
            }
            var hash = Rfc2898DeriveBytes.Pbkdf2(
                password,
                salt,
                iterations: 350000,
                outputLength: 32,
                hashAlgorithm: HashAlgorithmName.SHA512); // Hash the password with the salt.
            var hashedBytes = new byte[48];
            Array.Copy(salt, 0, hashedBytes, 0, salt.Length); // Combine the salt and hash.
            Array.Copy(hash, 0, hashedBytes, salt.Length, hash.Length);
            var hashedPassword = Convert.ToBase64String(hashedBytes);
            return hashedPassword;
        }

        public static bool VerifyPassword(string password, string hash)
        {
            byte[] hashBytes = Convert.FromBase64String(hash);
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, salt.Length); // Extract the salt from the hash.
            var newHash = Rfc2898DeriveBytes.Pbkdf2(
                password,
                salt,
                iterations: 350000,
                outputLength: 32,
                hashAlgorithm: HashAlgorithmName.SHA512); // Hash the password with the salt.

            for (int i = 0; i < newHash.Length; i++)
            {
                if (hashBytes[i + salt.Length] != newHash[i])
                {
                    return false; // The password is incorrect.
                }
            }
            return true; // The password is correct.
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserModel model)
        {
            var user = _ctx.Users.FirstOrDefault(u => u.Email == model.Email);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            if (!VerifyPassword(model.Password, user.PasswordHash))
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