global using Xunit;
using Moq;
using UniversityProgram.BLL.ErrorCodes;
using UniversityProgram.BLL.Services.StudentServices;
using UniversityProgram.Domain.BaseRepositories;
using UniversityProgram.Domain.BaseRepositories.CourseRepBase;
using UniversityProgram.Domain.BaseRepositories.StudentRepBase;
using UniversityProgram.Domain.Entities;

namespace UniversityProgram.BLL.Tests
{
    public class StudentServiceTests
    {
        private readonly IStudentService _studentService;
        private readonly Mock<IStudentRepository> _studentRepositoryMock = new Mock<IStudentRepository>(MockBehavior.Strict);
        private readonly Mock<ICourseStudentRepository> _courseStudentRepositoryMock = new Mock<ICourseStudentRepository>(MockBehavior.Strict);
        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new Mock<IUnitOfWork>(MockBehavior.Strict);

        public StudentServiceTests()
        {
            _studentService = new StudentService(_unitOfWorkMock.Object);
        }


        [Fact]
        public async Task Payment_Success()
        {
            //Arrange
            const int studentId = 1;
            const int courseId = 13;
            const decimal studentMoney = 5000;
            const decimal courseFee = 3000;
            const decimal studentUpdatedMoney = studentMoney - courseFee;
            var student = new Domain.Entities.StudentBase
            {
                Id = studentId,
                Money = studentMoney
            };

            var courseStudent = new CourseStudent
            {
                StudentId =studentId,
                CourseId = courseId,
                Course = new Course()
                {Id=courseId,
                Fee=courseFee}
            };

            _unitOfWorkMock.Setup(e => e.StudentRepository).Returns(_studentRepositoryMock.Object);
            _unitOfWorkMock.Setup(e => e.CourseStudentRepository).Returns(_courseStudentRepositoryMock.Object);

            _studentRepositoryMock.Setup(E => E.GetStudentByID(studentId, It.IsAny<CancellationToken>())).ReturnsAsync(student);
            _courseStudentRepositoryMock.Setup(e => e.GetByIds(studentId, courseId, It.IsAny<CancellationToken>())).ReturnsAsync(courseStudent);
            _studentRepositoryMock.Setup(e => e.UpdateStudent(It.Is<StudentBase>(e => e.Id == studentId && e.Money == studentUpdatedMoney)));
            _courseStudentRepositoryMock.Setup(e => e.UpdateCourseStudent(It.IsAny<CourseStudent>()));
            _unitOfWorkMock.Setup(e => e.Save(It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

            //Act
            var result = _studentService.Pay(studentId, courseId, CancellationToken.None).Result;

            //Assert
            Assert.True(result.Success);
            Assert.Equal("Course paid", result.Message);
            _courseStudentRepositoryMock.Verify(e => e.UpdateCourseStudent(It.Is<CourseStudent>(e=>e.Paid==true && e.StudentId==studentId 
            && e.CourseId == courseId)), times: Times.Once); //Lracucich partadir stugum ete uzum enq henc logikan stugenq.

        }

        [Fact]
        public async Task Payment_NullStudent_NotFoundResult()
        {
            //Arrange
            const int studentId = 1;
            _unitOfWorkMock.Setup(e => e.StudentRepository).Returns(_studentRepositoryMock.Object);
            _studentRepositoryMock.Setup(E => E.GetStudentByID(studentId, It.IsAny<CancellationToken>())).ReturnsAsync((StudentBase)null);
      
            //Act
            var result = await _studentService.Pay(studentId, 1000,CancellationToken.None);

            //Assert
            Assert.False(result.Success);
            Assert.Equal(ErrorType.NotFound, result.ErrorType);
        }
    }
}