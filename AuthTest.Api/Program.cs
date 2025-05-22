
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using UniversityProgram.Data;

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
            builder.Services.AddScoped<IAuthorizationHandler, TestRequirementHandler>();
            builder.Services.AddAuthentication(AuthScheme).AddCookie(AuthScheme).AddCookie("UrishCookie"); // default authentication scheme.

            builder.Services.AddDbContext<StudentDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("StudentDb")));

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Only Students", policy =>
                {
                    policy.AddAuthenticationSchemes(AuthScheme)
                    .RequireAuthenticatedUser()
                    .AddRequirements(new TestRequirement())
                    .RequireClaim("usertype", "student");

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
                }).RequireAuthorization("Only Students");

            app.MapGet("/login", async (HttpContext ctx) =>
            {
                var claim = new Claim("Email", "Karlos@gmail.com");
                var claim1 = new Claim("usertype", "student");
                var claimsIdentity = new ClaimsIdentity(new List<Claim>() { claim, claim1 }, AuthScheme);
                var user = new ClaimsPrincipal(claimsIdentity);
                await ctx.SignInAsync(AuthScheme, user);
                return Results.Ok("Login successful");
            }).AllowAnonymous();


            app.MapControllers();

            app.Run();
        }

        public class TestRequirement : IAuthorizationRequirement
        {
            public decimal Money { get; } = 3000;
        }
        
        public class TestRequirementHandler : AuthorizationHandler<TestRequirement>
        {
            private readonly StudentDbContext _ctx;

            public TestRequirementHandler(StudentDbContext ctx)
            {
                _ctx = ctx;
            }   
            protected override async Task HandleRequirementAsync(AuthorizationHandlerContext ctx, TestRequirement requirement)
            {
                var student = await _ctx.Students.FirstOrDefaultAsync(e=>e.Email==ctx.User.FindFirst("email")!.Value);
                if (student==null)
                {
                   ctx.Fail();
                   return;
                }

                if (student.Money >= requirement.Money)
                {
                    ctx.Succeed(requirement);
                }
                else
                {
                    ctx.Fail();
                }
            }
        }
    }
}

