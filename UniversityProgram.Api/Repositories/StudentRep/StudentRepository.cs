using Microsoft.EntityFrameworkCore;
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

        public async Task AddStudent(StudentBase student, CancellationToken token = default)
        {
            _ctx.Students.Add(student);
            await _ctx.SaveChangesAsync(token);

        }

        public async Task<IEnumerable<StudentBase>> GetStudents(CancellationToken token = default)
        {
            return await _ctx.Students.ToListAsync(token);
        }

        public async Task<StudentBase?> GetStudentByID(int Id, CancellationToken token = default)
        {
            return await _ctx.Students.FirstOrDefaultAsync(e => e.Id == Id, token);
        }

        public async Task UpdateStudent(StudentBase student, CancellationToken token = default)
        {
            _ctx.Students.Update(student);
            await _ctx.SaveChangesAsync(token);
        }

        public async Task DeleteStudent(StudentBase student, CancellationToken token = default)
        {
            _ctx.Students.Remove(student);
            await _ctx.SaveChangesAsync(token);
        }
    }
}
