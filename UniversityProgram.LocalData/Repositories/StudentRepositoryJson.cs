using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityProgram.Domain.BaseRepositories.StudentRepBase;
using UniversityProgram.Domain.Entities;

namespace UniversityProgram.LocalData.Repositories;
internal class StudentRepositoryJson : IStudentRepository
{
    private readonly IJsonDataService _jsonDataService;

    public StudentRepositoryJson(IJsonDataService jsonDataService)
    {
        _jsonDataService = jsonDataService;
    }
    public void AddStudent(StudentBase student, CancellationToken token = default)
    {
        _jsonDataService.WriteData(student);
    }

    public void DeleteStudent(StudentBase student, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<List<StudentBase>> GetAllStudentWithLaptop(CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<StudentBase?> GetByIdWithCourses(int Id, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<StudentBase?> GetByIdWithLaptop(int Id, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<StudentBase?> GetById_StudentWithLaptop(int Id, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<StudentBase?> GetStudentByID(int Id, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<StudentBase>> GetStudents(CancellationToken token = default)
    {
        var student = _jsonDataService.ReadData<StudentBase>();
        return Task.FromResult(new List<StudentBase> { student }.AsEnumerable());
    }

    public void UpdateStudent(StudentBase student)
    {
        throw new NotImplementedException();
    }
}
