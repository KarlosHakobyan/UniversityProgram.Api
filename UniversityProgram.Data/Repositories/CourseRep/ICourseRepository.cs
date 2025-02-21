using UniversityProgram.Data.Entities;

namespace UniversityProgram.Data.Repositories.CourseRep
{
    public interface ICourseRepository
    {
        void AddCourse(Course course, CancellationToken token = default);
        void DeleteCourse(Course course, CancellationToken token = default);
        Task<Course?> GetCourseByID(int Id, CancellationToken token = default);
        Task<IEnumerable<Course>> GetCourses(CancellationToken token = default);
        void UpdateCourse(Course course, CancellationToken token = default);
    }
}