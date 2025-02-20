using UniversityProgram.Api.Entities;

namespace UniversityProgram.Api.Repositories.StudentRep
{
    public interface IStudentRepository
    {
        void AddStudent(StudentBase student, CancellationToken token = default);
        void DeleteStudent(StudentBase student, CancellationToken token = default);
        Task<List<StudentBase>> GetAllStudentWithLaptop(CancellationToken token = default);
        Task<StudentBase?> GetByIdWithCourses(int Id, CancellationToken token = default);
        Task<StudentBase?> GetByIdWithLaptop(int Id, CancellationToken token = default);
        Task<StudentBase?> GetById_StudentWithLaptop(int Id, CancellationToken token = default);
        Task<StudentBase?> GetStudentByID(int Id, CancellationToken token = default);
        Task<IEnumerable<StudentBase>> GetStudents(CancellationToken token = default);
        void UpdateStudent(StudentBase student);
    }
}