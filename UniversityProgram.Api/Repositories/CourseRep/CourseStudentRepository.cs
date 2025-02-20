using Microsoft.EntityFrameworkCore;
using UniversityProgram.Api.Entities;

namespace UniversityProgram.Api.Repositories.CourseRep
{
    public class CourseStudentRepository : ICourseStudentRepository
    {
        private readonly StudentDbContext _ctx;

        public CourseStudentRepository(StudentDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<CourseStudent?> GetByIds(int studentId, int courseId, CancellationToken token = default!)
        {
            return await _ctx.CourseStudent
                .Include(e=>e.Course)
                .FirstOrDefaultAsync(e => e.StudentId == studentId && e.CourseId == courseId, token);

        }

        public async Task UpdateCourseStudent (CourseStudent courseStudent, CancellationToken token = default!)
        {
            _ctx.CourseStudent.Update(courseStudent);
            await _ctx.SaveChangesAsync(token);

        }

    }
}
