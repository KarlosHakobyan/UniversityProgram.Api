using Bunit;
using UniversityProgram.Blazor.Components;


namespace UniversityProgramBlazor.Test
{
    public class UnitTest1 : TestContext
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
    }
}