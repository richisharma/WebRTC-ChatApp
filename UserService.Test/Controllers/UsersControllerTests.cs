using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using UserService.Data;
using UserService.Models;
using Xunit;

namespace UserService.Test.Controllers
{
    public class UsersControllerTests
    {

        private readonly Mock<UserDbContext> _dbContextMock;
        private readonly UsersController _controller;

        public UsersControllerTests()
        {

            var options = new DbContextOptionsBuilder<UserDbContext>()
                .UseInMemoryDatabase(databaseName: "UsersDb")
                .Options;

            _dbContextMock = new Mock<UserDbContext>(options);

            _controller = new UsersController(_dbContextMock.Object);
        }

        [Fact]
        public void GetUsers_ShouldReturnUserList()
        {
            // Arrange
            var users = new List<ApplicationUser>
        {
            new ApplicationUser { Id = "1", FullName = "user1", Email = "user1@example.com" },
            new ApplicationUser { Id = "2", FullName = "user2", Email = "user2@example.com" }
        };

            var usersDbSetMock = GetMockDbSet(users);
            _dbContextMock.Setup(db => db.Users).Returns(usersDbSetMock.Object);

            // Act
            var result = _controller.GetUsers();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnUsers = Assert.IsType<List<ApplicationUser>>(okResult.Value);

            Assert.Equal(2, returnUsers.Count);
            Assert.Equal("user1", returnUsers[0].FullName);
            Assert.Equal("user2@example.com", returnUsers[1].Email);
        }

        private static Mock<DbSet<T>> GetMockDbSet<T>(List<T> sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();
            var mockDbSet = new Mock<DbSet<T>>();

            mockDbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            mockDbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            mockDbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            mockDbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());

            return mockDbSet;
        }
    }
}
