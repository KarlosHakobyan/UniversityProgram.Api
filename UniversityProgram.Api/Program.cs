
using Microsoft.EntityFrameworkCore;
using UniversityProgram.Api.Models;
using UniversityProgram.Api.Services;

namespace UniversityProgram.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);                    
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<StudentDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("StudentDb")));
            builder.Services.AddScoped<CourseBankSeviceApi>();

            var app = builder.Build();

            
            if (app.Environment.IsDevelopment()) 
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.MapGet("Test", () => 100);

            app.MapControllers();

            app.Run();
        }
    }
}