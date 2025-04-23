using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using UniversityProgram.Blazor.Apis;
using UniversityProgram.Blazor.Components;
using UniversityProgram.Blazor.Models;


namespace UniversityProgramBlazor.Test
{
    public class UnitTest1 : BunitContext
    {
        [Fact]
        public void Test1()
        {
            // Arrange
            var component = Render<NewTestPage>();
            var button = component.Find(".myButton");
            var textComponent = component.Find("#MyText");

            // Act
            button.Click();

            // Assert
            textComponent.MarkupMatches("<p id=\"MyText\">user name: Gago</p>");
        }



        [Fact]
        public void NewTestPage_GetStudentName_Success()
        {
            // Arrange
            Mock<IStudentApi> studentApiMock = new Mock<IStudentApi>();
            List <StudentModel> models = new List<StudentModel>
            {
                new StudentModel { Id = 1, Name = "Aram" },
                new StudentModel { Id = 2, Name = "Gago" }
            };
            studentApiMock.Setup(x => x.GetAll()).ReturnsAsync(models);
            Services.TryAddScoped(e=>studentApiMock.Object);

            var component = Render<NewTestPage>();
            var button = component.Find(".get-students");
            var textComponent = component.Find(".student-name");

            // Act
            button.Click();

            // Assert
            textComponent.MarkupMatches("<p class=\"student-name\">Aram</p>");
        }
    }
}