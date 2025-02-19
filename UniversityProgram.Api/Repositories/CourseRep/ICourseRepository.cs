using FluentValidation;
using UniversityProgram.Api.Entities;
using UniversityProgram.Api.Models.Course;

namespace UniversityProgram.Api.Repositories.CourseRep
{
    public interface ICourseRepository
    {
        Task AddCourse(Course course, CancellationToken token = default);
        Task DeleteCourse(Course course, CancellationToken token = default);
        Task DeleteCourseById(int id, CancellationToken cancellationToken);
        Task<Course?> GetCourseByID(int Id, CancellationToken token = default);
        Task<IEnumerable<Course>> GetCourses(CancellationToken token = default);
        Task UpdateCourse(Course course, CancellationToken token = default);                
    }
}