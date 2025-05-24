using BlazorWASM.Models;
using Refit;

namespace BlazorWASM.Apis
{
    public interface IStudentApi
    {
        [Get("/student/open")]
        Task<StudentModel> GetOpenModel();

        [Get("/student/private")]
        Task<StudentModel> GetPrivateStudentModel();

        [Get("/student")]
        Task<StudentModel> GetStudentById([Query] int id);

        [Delete("/student")]
        Task<StudentModel> DeleteStudent(int id);

        [Post("/student")]
        Task<StudentModel> AddStudent([Body] StudentModel model,CancellationToken tkn= default);
    }
}
