using UniversityProgram.Data.Entities;

namespace UniversityProgram.Data.Repositories.CourseRep
{
    public interface ICourseStudentRepository
    {
        Task<CourseStudent?> GetByIds(int studentId, int courseId, CancellationToken token = default);
        void UpdateCourseStudent(CourseStudent courseStudent);
    }
}