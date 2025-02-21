using Microsoft.AspNetCore.Mvc;
using UniversityProgram.Api.Entities;
using UniversityProgram.Api.Map;
using UniversityProgram.Api.Models;
using UniversityProgram.Api.Models.Student;
using UniversityProgram.Api.ErrorCodes;
using UniversityProgram.Api.Repositories;

namespace UniversityProgram.Api.Services.StudentServices
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

        public async Task <IEnumerable<StudentModel>> GetAll(CancellationToken cancellationToken)
        {
            var students = await _uow.StudentRepository.GetStudents(cancellationToken);
            return (students.Select(e => e.Map()));
        }

        public async Task<StudentModel?> GetById(int Id, CancellationToken cancellationToken)
        {
            var student = await _uow.StudentRepository.GetStudentByID(Id, cancellationToken);
            return (student?.Map());
        }

        public async Task<StudentWithLaptopModel?> GetByIdWithLaptop(int Id, CancellationToken cancellationToken)
        {
            var student = await _uow.StudentRepository.GetByIdWithLaptop(Id, cancellationToken);
            return (student?.MapToStudentWithLaptop());
        }

        public async Task<Result> Update(int Id, StudentUpdateModel model,CancellationToken cancellationToken)
        {
            var student = await _uow.StudentRepository.GetStudentByID(Id, cancellationToken);
            if (student == null)
            {
                return new Result(false,ErrorCodesBase.NotFound);
            }
            student.Email = model.Email is not null ? model.Email : student.Email;
            _uow.StudentRepository.UpdateStudent(student);
            await _uow.Save(cancellationToken);
            return new Result(true, "Student updated");
        }

        public async Task<Result> Delete(int Id, CancellationToken cancellationToken)
        {
            var student = await _uow.StudentRepository.GetStudentByID(Id, cancellationToken);
            if (student == null)
            {
                return new Result(false, "Student not found");
            }
            _uow.StudentRepository.DeleteStudent(student, cancellationToken);
            await _uow.Save(cancellationToken);
            return new Result(true, "Student deleted");
        }

    }
}
