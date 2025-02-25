using UniversityProgram.Data.Repositories.CourseRep;
using UniversityProgram.Data.Repositories.StudentRep;
using UniversityProgram.Domain.BaseRepositories;
using UniversityProgram.Domain.BaseRepositories.CourseRepBase;
using UniversityProgram.Domain.BaseRepositories.StudentRepBase;

namespace UniversityProgram.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StudentDbContext _ctx;

        public UnitOfWork(StudentDbContext ctx)
        {
            _ctx = ctx;
            StudentRepository = new StudentRepository(_ctx);
            CourseRepository = new CourseRepository(_ctx);
            CourseStudentRepository = new CourseStudentRepository(_ctx);
        }

        public IStudentRepository StudentRepository { get; }
        public ICourseRepository CourseRepository { get; }
        public ICourseStudentRepository CourseStudentRepository { get; }

        public async Task Save(CancellationToken token)
        {
            await _ctx.SaveChangesAsync(token);
        }

        public void Dispose()
        {
            _ctx.Dispose();
        }
    }
}
