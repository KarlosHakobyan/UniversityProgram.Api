
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using UniversityProgram.Api.Validators.CourseValidations;
using UniversityProgram.Api.Validators.LaptopValidations;
using UniversityProgram.Api.Validators.StudentValidations;
using UniversityProgram.Api.Map;
using UniversityProgram.BLL.Models.Laptop;
using UniversityProgram.Data;
using UniversityProgram.Data.Repositories;
using UniversityProgram.BLL.Services.StudentServices;
using UniversityProgram.Domain.BaseRepositories;
using UniversityProgram.LocalData.Repositories;
using UniversityProgram.LocalData;
using UniversityProgram.Api.Hubs;

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
            builder.Services.AddScoped<IValidator<LaptopAddModel>, LaptopAddModelValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<LaptopAddModelValidator>(ServiceLifetime.Transient);
            builder.Services.AddValidatorsFromAssemblyContaining<CourseValidator>(ServiceLifetime.Transient);
            builder.Services.AddValidatorsFromAssemblyContaining<StudentBaseValidator>(ServiceLifetime.Transient);
            builder.Services.AddAutoMapper(typeof(LaptopProfile));
            /*      builder.Services.AddScoped<ICourseRepository, CourseRepository>();
                    builder.Services.AddScoped<IStudentRepository, StudentRepository>();
                    builder.Services.AddScoped<ICourseStudentRepository, CourseStudentRepository>();

                    UnitOfWork kirarelu depqum es repositorynery jnjvum en */

            // builder.Services.AddScoped<IUnitOfWork, UnitOfWorkJson>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IStudentService, StudentService>();
            builder.Services.AddScoped<IJsonDataService, JsonDataService>();
            builder.Services.AddSignalR();




            var app = builder.Build();



            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseCors(policy=> 
            policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            );

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.MapGet("Test", () => 100);
            app.MapHub<StudentHub>("/studentHub");

            app.MapControllers();

            app.Run();
        }
    }
}