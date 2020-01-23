using System;
using weblab2.Data;
using Xunit;

namespace XUnitIntegrationTestWeblab2
{
    public class UnitTest2_Pomelo_EntityFramework_Core_MySql_ApplicationDbContextTest
    {
        [Fact]
        public void Application_DbContext_Should_Connect_To_MySql_Database()
        {
            // Arrange
            ApplicationDbContext DbContext = new ApplicationDbContext();
            bool expected = true;
            // Act
            bool result = DbContext.Database.CanConnect();

            // Assert
            Assert.Equal(expected, result);
        }
        [Fact]
        public async void Application_DbContext_Should_Connect_To_MySql_Database_Async()
        {
            // Arrange
            ApplicationDbContext DbContext = new ApplicationDbContext();
            bool expected = true;
            // Act
            bool result = await DbContext.Database.CanConnectAsync();

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
