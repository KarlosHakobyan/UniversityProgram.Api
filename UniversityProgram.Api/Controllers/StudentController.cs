using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using UniversityProgram.BLL.Services.StudentServices;
using UniversityProgram.BLL.ErrorCodes;
using UniversityProgram.BLL.Models.Student;
using UniversityProgram.Domain.Entities;
using Microsoft.AspNetCore.SignalR;
using UniversityProgram.Api.Hubs;

namespace UniversityProgram.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly IHubContext<StudentHub> _hubContext;

        public StudentController(IStudentService studentService,IHubContext<StudentHub>hubContext)
        {
            _studentService = studentService;
            _hubContext = hubContext;
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
            await Task.Delay(2000, cancellationToken);
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
            var result = await _studentService.GetByIdWithLaptop(Id, cancellationToken);
            if (!result.Success)
            {
                if (result.ErrorType == ErrorType.NotFound)
                {
                    return NotFound("Student not found");
                }
                if (result.ErrorType == ErrorType.LaptopNotFound)
                {
                    return BadRequest($"Student with Id->{Id} doesn't have a laptop");
                }

            }

            return Ok(result.Data);
        }

        [HttpGet("{Id}/course")]
        public async Task<IActionResult> GetWithCourses([FromRoute] int Id, CancellationToken cancellationToken)
        {
            var result = await _studentService.GetStudentByIdWithCourses(Id, cancellationToken);
            if (result.ErrorType == ErrorType.NotFound)
            {
                return NotFound("Student not found");
            }

            return Ok(result.Data);
        }
        /*
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
                if (result.ErrorType == ErrorType.NotFound)
                {
                    return NotFound();
                }
                return BadRequest(result.Message);
            }           
            await _hubContext.Clients.All.SendAsync("UpdateMessage", $"Student with ID: {Id} data was updated.");
            await _hubContext.Clients.All.SendAsync("OnUpdate", $"Students list was updated.");
            return Ok(result);

        }

        [HttpPut("{Id}/addmoney")]
        public async Task<IActionResult> AddMoney([FromRoute] int Id, [FromQuery] decimal money,
            [FromServices] IValidator<StudentBase> validator,
            CancellationToken cancellationToken)
        {
            

            var validResult = await validator.ValidateAsync(new StudentBase { Money = money }, cancellationToken);
            
            if (!validResult.IsValid)
            {
                return BadRequest(validResult.Errors);
            }

            var result = await _studentService.AddMoney(Id, money, cancellationToken);
            if (result.ErrorType == ErrorType.NotFound)
            {
                return NotFound();
            }
            return Ok();
        }



        ///***********************************>>>vvv PAYMENT SYSTEM (BL) vvv<<<****************************************

        [HttpPut("{Id}/pay/{courseId}")]
        public async Task<IActionResult> PayForCourse([FromRoute] int Id, [FromRoute] int courseId, CancellationToken cancellationToken)
        {
            var result = await _studentService.Pay(Id, courseId, cancellationToken);
           
                if (result.ErrorType == ErrorType.NotFound)
                {
                    return NotFound();
                }
                else if (result.ErrorType==ErrorType.CommonError)
                {
                    return BadRequest(result.Message);
                }            
                return Ok();
        }

        ///***********************************>>>^^^ PAYMENT SYSTEM  (BL) ^^^<<<****************************************

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
                if (result.ErrorType == ErrorType.NotFound)
                {
                    return NotFound();
                }
                return BadRequest(result.Message);
            }
            await _hubContext.Clients.All.SendAsync("DeleteMessage", $"Student with ID: {Id} was deleted.");
            await _hubContext.Clients.All.SendAsync("OnUpdate", $"Students list was updated.");
            return Ok(result);

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
