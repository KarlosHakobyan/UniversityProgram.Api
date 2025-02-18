using FluentValidation;
using UniversityProgram.Api.Entities;
using UniversityProgram.Api.Models.Course;

namespace UniversityProgram.Api.Repositories
{
    public interface ICourseRepository
    {
        Task AddCourse(Course course, CancellationToken token = default);
        Task<bool> DeleteCourseById(int Id, CancellationToken token = default);
        Task<Course?> GetCourseByID(int Id, CancellationToken token = default);
        Task<IEnumerable<Course>> GetCourses(CancellationToken token = default);
        Task<bool> UpdateCourseById(int Id, CourseUpdateModel model, CancellationToken token = default);
        Task<bool> UpdateFeeById(int Id, decimal fee, IValidator<Course> validator, CancellationToken token = default);
    }
}