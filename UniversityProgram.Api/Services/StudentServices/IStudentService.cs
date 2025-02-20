using UniversityProgram.Api.Models;
using UniversityProgram.Api.Models.Student;

namespace UniversityProgram.Api.Services.StudentServices
{
    public interface IStudentService
    {
        Task Add(StudentAddModel model, CancellationToken token = default);
        Task<Result> Delete(int Id, CancellationToken cancellationToken);
        Task<IEnumerable<StudentModel>> GetAll(CancellationToken cancellationToken);
        Task<StudentModel?> GetById(int Id, CancellationToken cancellationToken);
        Task<StudentWithLaptopModel?> GetByIdWithLaptop(int Id, CancellationToken cancellationToken);
        Task<Result> Update(int Id, StudentUpdateModel model, CancellationToken cancellationToken);
    }
}