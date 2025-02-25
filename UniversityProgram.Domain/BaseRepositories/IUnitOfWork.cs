using UniversityProgram.Domain.BaseRepositories.CourseRepBase;
using UniversityProgram.Domain.BaseRepositories.StudentRepBase;

namespace UniversityProgram.Domain.BaseRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        Task Save(CancellationToken token);

        IStudentRepository StudentRepository { get; }
        ICourseRepository CourseRepository { get; }
        ICourseStudentRepository CourseStudentRepository { get; }
    }
}