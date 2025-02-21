using UniversityProgram.Data.Repositories.CourseRep;
using UniversityProgram.Data.Repositories.StudentRep;

namespace UniversityProgram.Data.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        Task Save(CancellationToken token);

        IStudentRepository StudentRepository { get; }
        ICourseRepository CourseRepository { get; }
        ICourseStudentRepository CourseStudentRepository { get; }
    }
}