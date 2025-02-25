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
        _jsonDataService.Add(student);
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

    public async Task<IEnumerable<StudentBase>> GetStudents(CancellationToken token = default)
    {
        var student = await _jsonDataService.GetAllStudents();
        return student;
    }

    public void UpdateStudent(StudentBase student)
    {
        throw new NotImplementedException();
    }
}
