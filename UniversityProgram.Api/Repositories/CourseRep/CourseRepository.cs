using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using UniversityProgram.Api.Entities;
using UniversityProgram.Api.Models.Course;

namespace UniversityProgram.Api.Repositories.CourseRep
{
    public class CourseRepository : ICourseRepository
    {
        private readonly StudentDbContext _context;

        public CourseRepository(StudentDbContext context)
        {
            _context = context;
        }

        public async Task AddCourse(Course course, CancellationToken token = default)
        {
            _context.Courses.Add(course);
        }

        public async Task<IEnumerable<Course>> GetCourses(CancellationToken token = default)
        {
            return await _context.Courses.ToListAsync(token);
        }

        public async Task<Course?> GetCourseByID(int Id, CancellationToken token = default)
        {
            return await _context.Courses.FirstOrDefaultAsync(e => e.Id == Id, token);
        }

        public async Task<bool> UpdateCourseById(int Id, CourseUpdateModel model, CancellationToken token = default)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(e => e.Id == Id, token);
            if (course == null)
            {
                return false;
            }
            course.Name = model.Name;
            return true;
        }

        public async Task<bool> UpdateFeeById(int Id, decimal fee, IValidator<Course> validator, CancellationToken token = default)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(e => e.Id == Id, token);
            if (course == null)
            {
                return false;
            }
            course.Fee = fee;

            var validationResult = await validator.ValidateAsync(course, token);

            if (!validationResult.IsValid)
            {
                return false;
            }            
            return true;
        }


        public async Task<bool> DeleteCourseById(int Id, CancellationToken token = default)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(e => e.Id == Id, token);
            if (course == null)
            {
                return false;
            }
            _context.Courses.Remove(course);            
            return true;
        }

        public void DeleteCourse(Course course, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public void UpdateCourse(Course course, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }
    }
}
