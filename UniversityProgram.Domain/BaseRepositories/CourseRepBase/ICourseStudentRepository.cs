using UniversityProgram.Domain.Entities;

namespace UniversityProgram.Domain.BaseRepositories.CourseRepBase
{
    public interface ICourseStudentRepository
    {
        Task<CourseStudent?> GetByIds(int studentId, int courseId, CancellationToken token = default);
        void UpdateCourseStudent(CourseStudent courseStudent);
    }
}