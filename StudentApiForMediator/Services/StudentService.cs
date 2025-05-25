using StudentApiForMediator.Data;
using StudentApiForMediator.Models;

namespace StudentApiForMediator.Services
{
    public class StudentService
    {
        private readonly Database _database;

        public StudentService(Database database)
        {
            _database = database;
        }
        public void AddStudent(StudentAddModel model)
        {
            var student = new Student()
            {
                Email = model.Email,
                Name = model.Name,
                Id = model.Id
            };
            _database.Students.Add(student);
        }
    }
}
