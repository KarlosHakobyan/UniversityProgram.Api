using UniversityProgram.BLL.ErrorCodes;
using UniversityProgram.BLL.Models;
using UniversityProgram.BLL.Models.Student;
using UniversityProgram.BLL.Map;
using UniversityProgram.Data.Repositories;

namespace UniversityProgram.BLL.Services.StudentServices
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _uow;

        public StudentService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task Add(StudentAddModel model, CancellationToken token = default!)
        {
            var student = model.Map();
            _uow.StudentRepository.AddStudent(student, token);
            await _uow.Save(token);
        }

        public async Task<IEnumerable<StudentModel>> GetAll(CancellationToken cancellationToken)
        {
            var students = await _uow.StudentRepository.GetStudents(cancellationToken);
            return students.Select(e => e.Map());
        }

        public async Task<StudentModel?> GetById(int Id, CancellationToken cancellationToken)
        {
            var student = await _uow.StudentRepository.GetStudentByID(Id, cancellationToken);
            return student?.Map();
        }

        public async Task<Result<StudentWithLaptopModel>> GetByIdWithLaptop(int Id, CancellationToken cancellationToken)
        {
            var student = await _uow.StudentRepository.GetByIdWithLaptop(Id, cancellationToken);
            if (student == null)
            {
                return Result<StudentWithLaptopModel>.Error(ErrorType.NotFound);
            }

            if (student.Laptop == null)
            {
                return Result<StudentWithLaptopModel>.Error(ErrorType.LaptopNotFound);
            }

            var data = student.MapToStudentWithLaptop();

            return Result<StudentWithLaptopModel>.Ok(data);
        }

        public async Task<Result<StudentWithCourseModel>> GetStudentByIdWithCourses(int Id, CancellationToken token)
        {
            var student = await _uow.StudentRepository.GetByIdWithCourses(Id, token);
            if (student == null)
            {
                return Result<StudentWithCourseModel>.Error(ErrorType.NotFound);
            }

            return Result<StudentWithCourseModel>.Ok(student.MapStudentWithCourseModel());
        }

        public async Task<Result> Update(int Id, StudentUpdateModel model, CancellationToken cancellationToken)
        {
            var student = await _uow.StudentRepository.GetStudentByID(Id, cancellationToken);
            if (student == null)
            {
                return Result.Error(ErrorType.NotFound, "Student not found");
            }
            student.Email = model.Email is not null ? model.Email : student.Email;
            _uow.StudentRepository.UpdateStudent(student);
            await _uow.Save(cancellationToken);
            return Result.Ok("Student updated");
        }

        public async Task<Result> AddMoney(int Id, decimal money, CancellationToken token)
        {
            var student = await _uow.StudentRepository.GetStudentByID(Id, token);
            if (student == null)
            {
                return Result.Error(ErrorType.NotFound);
            }
            student.Money += money;
            _uow.StudentRepository.UpdateStudent(student);
            await _uow.Save(token);
            return Result.Ok();
        }

        public async Task<Result> Pay(int studentId, int courseId, CancellationToken cancellationToken)
        {
            var student = await _uow.StudentRepository.GetStudentByID(studentId, cancellationToken);

            if (student == null)
            {
                return Result.Error(ErrorType.NotFound);
            }

            var courseStudent = await _uow.CourseStudentRepository.GetByIds(studentId, courseId, cancellationToken);
            if (courseStudent == null)
            {
                return Result.Error(ErrorType.NotFound);
            }

            if (student.Money < courseStudent.Course.Fee)
            {
                return Result.Error(ErrorType.CommonError, "Not enough money");
            }
            else student.Money -= courseStudent.Course.Fee;
            _uow.StudentRepository.UpdateStudent(student);
            courseStudent.Paid = true;
            _uow.CourseStudentRepository.UpdateCourseStudent(courseStudent);
            await _uow.Save(cancellationToken);
            return Result.Ok("Course paid");
        }
        public async Task<Result> Delete(int Id, CancellationToken cancellationToken)
        {
            var student = await _uow.StudentRepository.GetStudentByID(Id, cancellationToken);
            if (student == null)
            {
                return Result.Error(ErrorType.NotFound, "Student not found");
            }
            _uow.StudentRepository.DeleteStudent(student, cancellationToken);
            await _uow.Save(cancellationToken);
            return Result.Ok("Student deleted");
        }
    }
}
