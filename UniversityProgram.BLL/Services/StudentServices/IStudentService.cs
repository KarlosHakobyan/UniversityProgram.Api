using UniversityProgram.BLL.Models;
using UniversityProgram.BLL.Models.Student;

namespace UniversityProgram.BLL.Services.StudentServices
{
    public interface IStudentService
    {
        Task Add(StudentAddModel model, CancellationToken token = default);
        Task<Result> AddMoney(int Id, decimal money, CancellationToken token);
        Task<Result> Delete(int Id, CancellationToken cancellationToken);
        Task<IEnumerable<StudentModel>> GetAll(CancellationToken cancellationToken);
        Task<StudentModel?> GetById(int Id, CancellationToken cancellationToken);
        Task<Result<StudentWithLaptopModel>> GetByIdWithLaptop(int Id, CancellationToken cancellationToken);
        Task<Result<StudentWithCourseModel>> GetStudentByIdWithCourses(int Id, CancellationToken token);
        Task<Result> Pay(int studentId, int courseId, CancellationToken cancellationToken);
        Task<Result> Update(int Id, StudentUpdateModel model, CancellationToken cancellationToken);
    }
}