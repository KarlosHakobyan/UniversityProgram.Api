using UniversityProgram.Blazor.Models;

namespace UniversityProgram.Blazor.Apis
{
    public interface IStudentApi
    {
        Task<IEnumerable<StudentModel>> GetAll();
        Task<StudentModel> GetById(int Id);
        Task UpdateStudent(int Id, StudentUpdateModel model);
        Task Delete(int Id);
    }
}