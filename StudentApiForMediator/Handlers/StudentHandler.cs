using Mediator;
using StudentApiForMediator.Data;
using StudentApiForMediator.Models;
using StudentApiForMediator.Requests;

namespace StudentApiForMediator.Services
{
    public class StudentHandler : IRequestHandler<StudentAddRequest,StudentAddResponse>
    {
        private readonly Database _db;

        public StudentHandler(Database db)
        {
            _db = db;
        }
        public ValueTask<StudentAddResponse> Handle(StudentAddRequest request, CancellationToken tkn)
        { 
            var student = new Student
            {
                Id = request.Id,
                Name = request.Name,
                Email = request.Email
            };
            _db.Students.Add(student);

            var response = new StudentAddResponse
            {
                Id = student.Id,
                Success = true
            };

            return ValueTask.FromResult(response);
        }
    }
}
