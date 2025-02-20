using UniversityProgram.Api.Entities;

namespace UniversityProgram.Api.Repositories.CourseRep
{
    public interface ICourseStudentRepository
    {
        Task<CourseStudent?> GetByIds(int studentId, int courseId, CancellationToken token = default);
        void UpdateCourseStudent(CourseStudent courseStudent);
    }
}