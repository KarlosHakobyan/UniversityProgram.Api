using Microsoft.EntityFrameworkCore;
using System.Threading;
using UniversityProgram.Api.Entities;

namespace UniversityProgram.Api.Repositories.StudentRep
{
    public class StudentRepository : IStudentRepository
    {
        private readonly StudentDbContext _ctx;

        public StudentRepository(StudentDbContext ctx)
        {
            _ctx = ctx;
        }

        public void AddStudent(StudentBase student, CancellationToken token = default)
        {
            _ctx.Students.Add(student);
        }

        public async Task<IEnumerable<StudentBase>> GetStudents(CancellationToken token = default)
        {
            return await _ctx.Students.ToListAsync(token);
        }

        public async Task<StudentBase?> GetStudentByID(int Id, CancellationToken token = default)
        {
            return await _ctx.Students.FirstOrDefaultAsync(e => e.Id == Id, token);
        }

        public void UpdateStudent(StudentBase student)
        {
            _ctx.Students.Update(student);
        }

        public void DeleteStudent(StudentBase student, CancellationToken token = default)
        {
            _ctx.Students.Remove(student);          
        }

        public async Task<StudentBase?> GetByIdWithLaptop(int Id, CancellationToken token = default)
        {
            return await _ctx.Students
                .Include(e => e.Laptop)
                    .ThenInclude(e => e.Cpu)
                        .FirstOrDefaultAsync(e => e.Id == Id,token);
        }

        public async Task<StudentBase?> GetByIdWithCourses(int Id, CancellationToken token = default)
        {
            return await _ctx.Students                
                .Include(e => e.CourseStudents)
                    .ThenInclude(e => e.Course)
                        .FirstOrDefaultAsync(e => e.Id == Id, token);
        }
    }
}
