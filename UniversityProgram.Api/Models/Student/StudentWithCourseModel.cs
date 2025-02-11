using UniversityProgram.Api.Models.Course;

namespace UniversityProgram.Api.Models.Student
{
    public class StudentWithCourseModel : StudentModel
    {
        public List<CourseModel> Courses { get; set; } = new List<CourseModel>();
    }
}
