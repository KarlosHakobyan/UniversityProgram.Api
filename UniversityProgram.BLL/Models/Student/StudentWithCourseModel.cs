using UniversityProgram.BLL.Models.Course;

namespace UniversityProgram.BLL.Models.Student
{
    public class StudentWithCourseModel : StudentModel
    {
        public List<CourseModel> Courses { get; set; } = new List<CourseModel>();
    }
}
