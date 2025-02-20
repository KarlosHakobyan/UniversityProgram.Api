using Azure.Messaging;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using UniversityProgram.Api.Entities;
using UniversityProgram.Api.Map;
using UniversityProgram.Api.Models.Course;
using UniversityProgram.Api.Models.CPU;
using UniversityProgram.Api.Models.Laptop;
using UniversityProgram.Api.Models.Student;
using UniversityProgram.Api.Repositories;
using UniversityProgram.Api.Repositories.CourseRep;
using UniversityProgram.Api.Repositories.StudentRep;
using UniversityProgram.Api.Services;
using UniversityProgram.Api.Services.StudentServices;

namespace UniversityProgram.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        #region HTTPPOST`s
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] StudentAddModel model, CancellationToken cancellationToken)
        {
            await _studentService.Add(model, cancellationToken);
            return Ok();
        }

        /*[HttpPost("/Student/add_with_laptop")]
        public async Task<IActionResult> AddStudentWithLaptop([FromBody] StudentWithLaptopAddModel student, CancellationToken cancellationToken)
        {
            var studentEntity = new StudentBase
            {
                Name = student.Name,
                Email = student.Email,
                Laptop = student.Laptop == null ? null : new Laptop
                {
                    Id = student.Laptop.Id,
                    Name = student.Laptop.Name
                }
            };

            _uow.StudentRepository.AddStudent(studentEntity,cancellationToken);
            await _uow.Save(cancellationToken);
            return Ok();
        }*/


        #endregion

        #region HPPTGET`s
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var students = await _studentService.GetAll(cancellationToken);
            return Ok(students);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] int Id, CancellationToken cancellationToken)
        {
            var student = await _studentService.GetById(Id, cancellationToken);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        [HttpGet("{Id}/Laptop")]
        public async Task<IActionResult> GetByIdWithLaptop([FromRoute] int Id, CancellationToken cancellationToken)
        {
            var student = await _studentService.GetByIdWithLaptop(Id, cancellationToken);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }

       /* [HttpGet("{Id}/course")]
        public async Task<IActionResult> GetWithCourses([FromRoute] int Id, CancellationToken cancellationToken)
        {
            var student = await _uow.StudentRepository.GetByIdWithCourses(Id, cancellationToken);
            if (student == null)
            {
                return NotFound();
            }

            return Ok(student.MapStudentWithCourseModel());
        }

        [HttpGet("with_laptop")]
        public async Task<IActionResult> GetAllStudentWithLaptop(CancellationToken cancellationToken)
        {
            var students = await _uow.StudentRepository.GetAllStudentWithLaptop(cancellationToken);

            if (students == null)
            {
                return NotFound();
            }

            var studentModels = students.Select(student => new StudentWithLaptopModel
            {
                Id = student.Id,
                Name = student.Name,
                Email = student.Email,
                Laptop = student.Laptop is not null
                    ? new LaptopWithCpuModel
                    {
                        Id = student.Laptop.Id,
                        Name = student.Laptop.Name,
                    }
                    : null
            }).ToList();

            return Ok(studentModels);
        }
*/
/*
        [HttpGet("{Id}/with_laptop")]
        public async Task<IActionResult> GetById_StudentWithLaptop([FromRoute] int Id, CancellationToken cancellationToken)
        {
            var student = await _uow.StudentRepository.GetById_StudentWithLaptop(Id, cancellationToken);
            if (student == null)
            {
                return NotFound();
            }

            return Ok(student.MapToStudentWithLaptop());
        }*/
        #endregion

        #region HTTPPUT`s

        [HttpPut("{Id}")]
        public async Task<IActionResult> Update([FromRoute] int Id, [FromBody] StudentUpdateModel model, CancellationToken cancellationToken)
        {
            var result = await _studentService.Update(Id, model, cancellationToken);
            if (!result.Success)
            {
                return NotFound(result.Message);
            }
            else return Ok(result);

        }
/*
        [HttpPut("{Id}/addmoney")]
        public async Task<IActionResult> AddMoney([FromRoute] int Id, [FromQuery] decimal money,
            [FromServices] IValidator<StudentBase> validator,
            CancellationToken cancellationToken)
        {
            var student = await _uow.StudentRepository.GetStudentByID(Id,cancellationToken);
            if (student == null)
            {
                return NotFound();
            }

            var result = await validator.ValidateAsync(new StudentBase { Money = money },cancellationToken);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }


            student.Money += money;
            _uow.StudentRepository.UpdateStudent(student);
            await _uow.Save(cancellationToken);
            return Ok();
        }
*/
        ///***********************************>>>vvv PAYMENT SYSTEM vvv<<<****************************************
/*
        [HttpPut("{Id}/pay/{courseId}")]
        public async Task<IActionResult> PayForCourse([FromRoute] int Id, [FromRoute] int courseId,CancellationToken cancellationToken)
        {
                var student = await _uow.StudentRepository.GetStudentByID(Id,cancellationToken);

                if (student == null)
                {
                    return NotFound();
                }

                var courseStudent = await _uow.CourseStudentRepository.GetByIds(Id, courseId, cancellationToken);
                if (courseStudent == null)
                {
                    return NotFound();
                }

                if (student.Money < courseStudent.Course.Fee)
                {
                    return BadRequest("Not enough money");
                }
                else student.Money -= courseStudent.Course.Fee;
                _uow.StudentRepository.UpdateStudent(student);
                courseStudent.Paid = true;
                _uow.CourseStudentRepository.UpdateCourseStudent(courseStudent);
                await _uow.Save(cancellationToken);
                return Ok();
        }*/

        ///***********************************>>>^^^ PAYMENT SYSTEM ^^^<<<****************************************

        /*[HttpPut("{Id}/course")]
        public async Task<IActionResult> AddCourse([FromRoute] int Id, [FromQuery] int courseId, CancellationToken cancellationToken)
        {
            var student = await _uow.StudentRepository.GetStudentByID(Id,cancellationToken);
            if (student == null)
            {
                return NotFound();
            }

            var course = await _uow.CourseRepository.GetCourseByID(courseId,cancellationToken);
            if (course == null)
            {
                return NotFound();
            }

            var courseStudent = new CourseStudent()
            {
                Course = course,
                Student = student
            };

            student.CourseStudents.Add(courseStudent);
            _uow.StudentRepository.UpdateStudent(student);
            await _uow.Save(cancellationToken);
            return Ok();
        }

        [HttpPut("{Id}/update_swl")]
        public async Task<IActionResult> UpdateById_StudentWithLaptop([FromRoute] int Id, [FromBody] StudentWithLaptopUpdateModel studentModel, CancellationToken cancellationToken)
        {
            var student = await _uow.StudentRepository.GetById_StudentWithLaptop(Id, cancellationToken);

            if (student == null)
            {
                return NotFound();
            }


            student.Name = studentModel.Name;
            student.Email = studentModel.Email;
            student.Laptop.Name = studentModel.Laptop.Name;
            _uow.StudentRepository.UpdateStudent(student);
            await _uow.Save(cancellationToken);
            return Ok(studentModel);
        }*/

        #endregion

        #region HTTPDELETE`s

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] int Id, CancellationToken cancellationToken)
        {
            var result = await _studentService.Delete(Id, cancellationToken);
            if (!result.Success)
            {
                return NotFound(result.Message);
            }
            else return Ok(result);
        }
/*
        [HttpDelete("{Id}/with_laptop")]
        public async Task<IActionResult> Delete_StudentWithLaptop([FromRoute] int Id, CancellationToken cancellationToken)
        {
            var student = await _uow.StudentRepository.GetById_StudentWithLaptop(Id, cancellationToken);
            if (student == null)
            {
                return NotFound();
            }
            _uow.StudentRepository.DeleteStudent(student);
            await _uow.Save(cancellationToken);
            return Ok();
        }*/
        #endregion



        /* 
        * ID ov get anelu urish tarberak
        * [HttpGet]
        public async Task<IActionResult> GetById(int Id)
        {
            var students = await _ctx.Students.FindAsync(Id);
            if (students == null)
        {
                return NotFound();
            }
            return Ok(students);
        }*/
    }
}
