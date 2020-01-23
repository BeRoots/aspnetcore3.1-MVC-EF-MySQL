using System;
using Xunit;

namespace XUnitIntegrationTestWeblab2
{
    public class UnitTest1_XunitText
    {
        [Fact]
        public void PassingTest()
        {
            // Arrange
            int a = 1;
            int b = 1;
            int expected = 2;

            // Act
            int result = a + b;

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void FailingTest()
        {
            // Arrange
            bool shouldFail = true; // Change to false to bypass failing this test

            int a = 1;
            int b = 1;
            int expected = (shouldFail) ? 3 : 2;

            // Act
            bool result = expected.Equals(a + b);

            // Assert
            Assert.False(result);
        }
    }
}
