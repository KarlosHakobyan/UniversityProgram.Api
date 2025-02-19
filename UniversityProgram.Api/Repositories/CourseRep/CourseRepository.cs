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
            await _context.SaveChangesAsync(token);
        }

        public async Task<IEnumerable<Course>> GetCourses(CancellationToken token = default)
        {
            return await _context.Courses.ToListAsync(token);
        }

        public async Task<Course?> GetCourseByID(int Id, CancellationToken token = default)
        {
            return await _context.Courses.FirstOrDefaultAsync(e => e.Id == Id, token);
        }

        public async Task UpdateCourse(Course course, CancellationToken token = default)
        {
            _context.Courses.Update(course);
            await _context.SaveChangesAsync(token);
        }

        public async Task DeleteCourseById(int Id, CancellationToken token = default)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(e => e.Id == Id, token);
      
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync(token);
        }

        public async Task DeleteCourse(Course course, CancellationToken token = default)
        {
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync(token);
        }
    }
}
