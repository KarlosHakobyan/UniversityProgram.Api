using UniversityProgram.Api.Entities;

namespace UniversityProgram.Api.Repositories.StudentRep
{
    public interface IStudentRepository
    {
        Task AddStudent(StudentBase student, CancellationToken token = default);
        Task DeleteStudent(StudentBase student, CancellationToken token = default);
        Task<StudentBase?> GetByIdWithCourses(int Id, CancellationToken token = default);
        Task<StudentBase?> GetByIdWithLaptop(int Id, CancellationToken token = default);
        Task<StudentBase?> GetStudentByID(int Id, CancellationToken token = default);
        Task<IEnumerable<StudentBase>> GetStudents(CancellationToken token = default);
        Task UpdateStudent(StudentBase student, CancellationToken token = default);
    }
}