using UniversityProgram.Api.Repositories.CourseRep;
using UniversityProgram.Api.Repositories.StudentRep;

namespace UniversityProgram.Api.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        Task Save(CancellationToken token);

        IStudentRepository StudentRepository { get; }
        ICourseRepository CourseRepository { get; }
        ICourseStudentRepository CourseStudentRepository { get; }
    }
}