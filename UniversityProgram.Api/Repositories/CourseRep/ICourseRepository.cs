using FluentValidation;
using UniversityProgram.Api.Entities;
using UniversityProgram.Api.Models.Course;

namespace UniversityProgram.Api.Repositories.CourseRep
{
    public interface ICourseRepository
    {
        Task AddCourse(Course course, CancellationToken token = default);
        void DeleteCourse(Course course, CancellationToken token = default);
        Task<bool> DeleteCourseById(int id, CancellationToken cancellationToken);
        Task<Course?> GetCourseByID(int Id, CancellationToken token = default);
        Task<IEnumerable<Course>> GetCourses(CancellationToken token = default);
        void UpdateCourse(Course course, CancellationToken token = default);
        Task<bool> UpdateCourseById(int id, CourseUpdateModel model, CancellationToken cancellationToken);
        Task<bool> UpdateFeeById(int id, decimal fee, IValidator<Course> validator, CancellationToken cancellationToken);
        /*Task<bool> UpdateCourseById(int Id, CourseUpdateModel model, CancellationToken token = default);*/
        /* Task<bool> UpdateFeeById(int Id, decimal fee, IValidator<Course> validator, CancellationToken token = default);*/
    }
}