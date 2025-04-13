using UniversityProgram.Blazor.Models;

namespace UniversityProgram.Blazor.Apis
{
    public interface IStudentApi
    {
        Task<IEnumerable<StudentModel>> GetAll();
    }
}