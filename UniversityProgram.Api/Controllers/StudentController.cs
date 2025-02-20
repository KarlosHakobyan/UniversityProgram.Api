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
using UniversityProgram.Api.Repositories.CourseRep;
using UniversityProgram.Api.Repositories.StudentRep;
using UniversityProgram.Api.Services;

namespace UniversityProgram.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ICourseRepository _courseRepository;

        public StudentController(IStudentRepository studentRepository,ICourseRepository courseRepository)
        {
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;
        }

        #region HTTPPOST`s
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] StudentAddModel model, CancellationToken cancellationToken)
        {
            var student = model.Map();
            await _studentRepository.AddStudent(student,cancellationToken);            
            return Ok();
        }

        [HttpPost("/Student/add_with_laptop")]
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

            await _studentRepository.AddStudent(studentEntity,cancellationToken);
             return Ok();
        }


        #endregion

        #region HPPTGET`s
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var students = await _studentRepository.GetStudents(cancellationToken);
            return Ok(students.Select(e => e.Map()));
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] int Id, CancellationToken cancellationToken)
        {
            var student = await _studentRepository.GetStudentByID(Id,cancellationToken);
            if (student == null)
            {
                return NotFound();
            }

            return Ok(student.Map());
        }

/*        [HttpGet("{Id}/Laptop")]
        public async Task<IActionResult> GetByIdWithLaptop([FromRoute] int Id, CancellationToken cancellationToken)
        {
            var student = await _ctx.Students.Include(e => e.Laptop)
                .ThenInclude(e => e.Cpu)
                .FirstOrDefaultAsync(e => e.Id == Id, cancellationToken);
            if (student == null)
            {
                return NotFound();
            }

            return Ok(student.MapToStudentWithLaptop());
        }

        [HttpGet("{Id}/course")]
        public async Task<IActionResult> GetWithCourses([FromRoute] int Id, CancellationToken cancellationToken)
        {
            var student = await _ctx.Students
                .Include(e => e.CourseStudents)
                .ThenInclude(e => e.Course)
                .FirstOrDefaultAsync(e => e.Id == Id, cancellationToken);
            if (student == null)
            {
                return NotFound();
            }

            return Ok(student.MapStudentWithCourseModel());
        }

        [HttpGet("with_laptop")]
        public async Task<IActionResult> GetAllStudentWithLaptop(CancellationToken cancellationToken)
        {
            var students = await _ctx.Students
                .Include(e => e.Laptop)
                .ToListAsync(cancellationToken);

            if (students == null || students.Count == 0)
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
        }*/


       /* [HttpGet("{Id}/with_laptop")]
        public async Task<IActionResult> GetById_StudentWithLaptop([FromRoute] int Id, CancellationToken cancellationToken)
        {
            var student = await _ctx.Students.Include(e => e.Laptop)
                .FirstOrDefaultAsync(e => e.Id == Id, cancellationToken);
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
            var student = await _studentRepository.GetStudentByID(Id,cancellationToken);
            if (student == null)
            {
                return NotFound();
            }
            student.Email = model.Email is not null ? model.Email : student.Email;
            await _studentRepository.UpdateStudent(student,cancellationToken);            
            return Ok();
        }

        [HttpPut("{Id}/addmoney")]
        public async Task<IActionResult> AddMoney([FromRoute] int Id, [FromQuery] decimal money,
            [FromServices] IValidator<StudentBase> validator,
            CancellationToken cancellationToken)
        {
            var student = await _studentRepository.GetStudentByID(Id,cancellationToken);
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
            await _studentRepository.UpdateStudent(student,cancellationToken);
            return Ok();
        }

        /*[HttpPut("{Id}/pay/{courseId}")]
        public async Task<IActionResult> PayForCourse([FromRoute] int Id, [FromRoute] int courseId,
            [FromServices] CourseBankSeviceApi bankApi, CancellationToken cancellationToken)
        {
            using var transaction = await _ctx.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                var student = await _ctx.Students
                    .Include(e => e.CourseStudents)
                    .ThenInclude(e => e.Course)
                    .FirstOrDefaultAsync(e => e.Id == Id, cancellationToken);

                if (student == null)
                {
                    return NotFound();
                }

                var courseStudent = student.CourseStudents.FirstOrDefault(e => e.CourseId == courseId);
                if (courseStudent == null)
                {
                    return NotFound();
                }

                if (student.Money < courseStudent.Course.Fee)
                {
                    return BadRequest("Not enough money");
                }
                else student.Money -= courseStudent.Course.Fee;

                courseStudent.Paid = true;
                await _ctx.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync(cancellationToken);
                return BadRequest(e.Message);
            }
            return Ok();
        }*/

        [HttpPut("{Id}/course")]
        public async Task<IActionResult> AddCourse([FromRoute] int Id, [FromQuery] int courseId, CancellationToken cancellationToken)
        {
            var student = await _studentRepository.GetStudentByID(Id,cancellationToken);
            if (student == null)
            {
                return NotFound();
            }

            var course = await _courseRepository.GetCourseByID(courseId,cancellationToken);
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
            await _studentRepository.UpdateStudent(student,cancellationToken);
            return Ok();
        }

        /*[HttpPut("{Id}/update_swl")]
        public async Task<IActionResult> UpdateById_StudentWithLaptop([FromRoute] int Id, [FromBody] StudentWithLaptopUpdateModel studentModel, CancellationToken cancellationToken)
        {
            var student = await _ctx.Students
                .Include(e => e.Laptop)
                .FirstOrDefaultAsync(e => e.Id == Id, cancellationToken);

            if (student == null)
            {
                return NotFound();
            }


            student.Name = studentModel.Name;
            student.Email = studentModel.Email;
            student.Laptop.Name = studentModel.Laptop.Name;

            await _ctx.SaveChangesAsync(cancellationToken);

            return Ok(studentModel);
        }*/

        #endregion

        #region HTTPDELETE`s

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] int Id, CancellationToken cancellationToken)
        {
            var student = await _studentRepository.GetStudentByID(Id,cancellationToken);
            if (student == null)
            {
                return NotFound();
            }
            await _studentRepository.DeleteStudent(student,cancellationToken);            
            return Ok();
        }
            
       /* [HttpDelete("{Id}/with_laptop")]
        public async Task<IActionResult> Delete_StudentWithLaptop([FromRoute] int Id, CancellationToken cancellationToken)
        {
            var student = await _ctx.Students.Include(e=>e.Laptop).FirstOrDefaultAsync(e => e.Id == Id, cancellationToken);
            if (student == null)
            {
                return NotFound();
            }
            _ctx.Students.Remove(student);
            await _ctx.SaveChangesAsync(cancellationToken);
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
