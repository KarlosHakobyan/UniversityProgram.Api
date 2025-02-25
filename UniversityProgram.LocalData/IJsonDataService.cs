using UniversityProgram.Domain.Entities;

namespace UniversityProgram.LocalData;

public interface IJsonDataService
{
    void Add(StudentBase student);
    Task<IEnumerable<StudentBase>> GetAllStudents();
    Task SaveChangesAsync();
}