using StudentApiForMediator.Data;

namespace StudentApiForMediator.Services
{
    public class CourseService
    {
        private Database _database;

        public CourseService(Database database)
        {
            _database = database;
        }

        public void AddCourse(int id, int studentId)
        {
            var course = _database.Courses.FirstOrDefault(b => b.Id == id);
            if (course == null)
            {
                throw new ArgumentException(nameof(id));
            }
            var student = _database.Students.FirstOrDefault(s => s.Id == studentId);
            if (student == null)
            {
                throw new ArgumentException(nameof(studentId));
            }
            course.Students.Add(student);
        }
    }
}