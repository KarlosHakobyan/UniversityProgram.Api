using UniversityProgram.BLL.Models.Course;
using UniversityProgram.BLL.Models.CPU;
using UniversityProgram.BLL.Models.Laptop;
using UniversityProgram.BLL.Models.Student;
using UniversityProgram.Data.Entities;

namespace UniversityProgram.BLL.Map
{
    public static class StudentMap
    {
        public static StudentWithLaptopModel MapToStudentWithLaptop(this StudentBase student)
        {
            return new StudentWithLaptopModel
            {
                Id = student.Id,
                Name = student.Name,
                Email = student.Email,
                Laptop = student.Laptop is not null
                        ? new LaptopWithCpuModel
                        {
                            Id = student.Laptop.Id,
                            Name = student.Laptop.Name,
                            Cpu = student.Laptop.Cpu is null ? null
                                : new CpuModel
                                {
                                    Id = student.Laptop.Cpu.Id,
                                    Name = student.Laptop.Cpu.Name
                                }
                        } : null

            };
        }
        public static StudentModel Map(this StudentBase student)
        {
            return new StudentModel
            {
                Id = student.Id,
                Name = student.Name,
                Email = student.Email,
                Money = student.Money
            };
        }

        public static StudentBase Map(this StudentAddModel student)
        {
            return new StudentBase
            {
                Name = student.Name,
                Email = student.Email

            };
        }


        public static StudentWithCourseModel MapStudentWithCourseModel(this StudentBase student)
        {
            var models = student.CourseStudents.Select(e => new CourseModel()
            {
                Id = e.Course.Id,
                Name = e.Course.Name,
                Fee = e.Course.Fee,
                Paid = e.Paid ? "Yes" : "No"
            }).ToList();
            var result = new StudentWithCourseModel()
            {
                Id = student.Id,
                Name = student.Name,
                Email = student.Email,
                Money = student.Money

            };
            result.Courses.AddRange(models);
            return result;
        }


    }
}
