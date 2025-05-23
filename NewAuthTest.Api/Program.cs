
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NewAuthTest.Api
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

            builder.Services.AddDbContext<IdentityDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityTestDb")));
            builder.Services.AddIdentity<IdentityUser, IdentityRole>(e =>
            {
                e.User.RequireUniqueEmail = true;
                e.Password.RequiredLength = 6;
                e.Password.RequireDigit = true;
                e.Password.RequireUppercase = true;
                e.Password.RequireLowercase = true;
            })
               .AddEntityFrameworkStores<IdentityDbContext>()
               .AddDefaultTokenProviders();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.MapGet("/seed", async (ctx) =>
            {
                var db=ctx.RequestServices.GetRequiredService<IdentityDbContext>();
                db.Database.EnsureCreated();

                var mng = ctx.RequestServices.GetRequiredService<UserManager<IdentityUser>>();
                var user = new IdentityUser()
                {
                    UserName = "testuser",
                    Email = "testemail@mail.ru"
                };
                var result = await mng.CreateAsync(user, "Aa#123");
                Console.WriteLine("AAA"); ;
            });

            app.MapControllers();

            app.Run();
        }
    }
}
