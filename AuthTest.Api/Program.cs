
using Microsoft.AspNetCore.DataProtection;

namespace AuthTest.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDataProtection();
            builder.Services.AddScoped<AuthService>();
            builder.Services.AddHttpContextAccessor();



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.Use((ctx,next) =>
            { 
                var provider = ctx.RequestServices.GetService<IDataProtectionProvider>();
                var authCookie = ctx.Request.Cookies["auth"];
                if (authCookie == null)
                {
                   throw new UnauthorizedAccessException("No auth cookie found");
                }
                var protector = provider.CreateProtector("auth-cookie");
                var unprotectedValue = protector.Unprotect(authCookie);

                return next.Invoke();
            });

            app.MapGet("/secret", (HttpContext ctx, IDataProtectionProvider provider) =>
            {                
                return "SECRET";
            });

            app.MapGet("/login", (AuthService auth) =>
            {
                auth.SignIn();
                return Results.Ok("Login successful");
            });


            app.MapControllers();

            app.Run();
        }

        public class AuthService
        {
            private readonly IHttpContextAccessor _accessor;
            private readonly IDataProtectionProvider _provider;

            public AuthService(IHttpContextAccessor accessor, IDataProtectionProvider provider)
            {
                _accessor = accessor;
                _provider = provider;
                
            }

            public void SignIn()
            {
                string cookieValue = "email:Karlos";
                var protector = _provider.CreateProtector("auth-cookie");
                var protectedValue = protector.Protect(cookieValue);
                _accessor.HttpContext!.Response.Headers.Add("Set-Cookie", $"auth={protectedValue}");
            }
        }
    }
}

