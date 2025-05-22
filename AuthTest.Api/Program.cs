
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection;
using System.Security.Claims;

namespace AuthTest.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            const string AuthScheme = "Cookie";
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAuthentication(AuthScheme).AddCookie(AuthScheme).AddCookie("UrishCookie"); // default authentication scheme.
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Only Students", policy =>
                {
                    policy.AddAuthenticationSchemes(AuthScheme)
                    .RequireAuthenticatedUser()
                    .RequireClaim("usertype","student");
                });
            });



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapGet("/secret", (HttpContext ctx, IDataProtectionProvider provider) =>
            {

                return ctx.User.FindFirst("email")?.Value;
            });

            app.MapGet("/studentTypeInfo", (HttpContext ctx) =>
                {
                    
                    return Results.Ok("Student name: Gago");
                }).RequireAuthorization("Only students");

            app.MapGet("/login", async (HttpContext ctx) =>
            {
                var claim = new Claim("Email", "Karlos@gmail.com");
                var claim1 = new Claim("usertype", "student");
                var claimsIdentity = new ClaimsIdentity(new List<Claim>() { claim, claim1 }, AuthScheme);
                var user = new ClaimsPrincipal(claimsIdentity);
                ctx.SignInAsync(user);
                return Results.Ok("Login successful");
            }).AllowAnonymous();


            app.MapControllers();

            app.Run();
        }

    }
}

