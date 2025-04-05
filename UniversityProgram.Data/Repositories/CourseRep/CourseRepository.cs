using Microsoft.EntityFrameworkCore;
using System.Threading;
using UniversityProgram.Domain.BaseRepositories.CourseRepBase;
using UniversityProgram.Domain.Entities;

namespace UniversityProgram.Data.Repositories.CourseRep
{
    public class CourseRepository : ICourseRepository
    {
        private readonly StudentDbContext _context;

        public CourseRepository(StudentDbContext context)
        {
            _context = context;
        }

        public void AddCourse(Course course, CancellationToken token = default)
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

        public void UpdateCourse(Course course, CancellationToken token = default)
        {
            _context.Courses.Update(course);
        }

        public void DeleteCourse(Course course, CancellationToken token = default)
        {
            _context.Courses.Remove(course);
        }
    }
}
