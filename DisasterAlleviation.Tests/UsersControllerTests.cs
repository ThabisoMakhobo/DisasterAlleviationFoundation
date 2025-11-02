using DisasterAlleviationFoundation.Controllers;
using DisasterAlleviationFoundation.Data;
using DisasterAlleviationFoundation.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DisasterAlleviation.Tests
{
    public class UsersControllerTests
    {
        private readonly Mock<UserManager<User>> _userManagerMock;
        private readonly GiftOfTheGiversDbContext _context;

        public UsersControllerTests()
        {
            // In-memory database setup
            var options = new DbContextOptionsBuilder<GiftOfTheGiversDbContext>()
                .UseInMemoryDatabase("UsersControllerTestDB")
                .Options;

            _context = new GiftOfTheGiversDbContext(options);

            // Mock UserManager dependencies
            var store = new Mock<IUserStore<User>>();
            _userManagerMock = new Mock<UserManager<User>>(
                store.Object, null, null, null, null, null, null, null, null);
        }

        [Fact]
        public async Task Index_ReturnsViewWithUsers()
        {
            // Arrange
            var users = new List<User>
            {
                new User { Id = "1", UserName = "john@example.com", Email = "john@example.com" },
                new User { Id = "2", UserName = "mary@example.com", Email = "mary@example.com" }
            }.AsQueryable();

            var mockUserList = new Mock<DbSet<User>>();
            _userManagerMock.Setup(x => x.Users).Returns(users);

            var controller = new UsersController(_context, _userManagerMock.Object);

            // Act
            var result = await controller.Index() as ViewResult;

            // Assert
            Assert.NotNull(result);
            var model = Assert.IsAssignableFrom<IEnumerable<User>>(result.Model);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public async Task Details_ReturnsView_WhenUserExists()
        {
            // Arrange
            var user = new User { Id = "1", Email = "john@example.com" };
            _userManagerMock.Setup(x => x.FindByIdAsync("1"))
                .ReturnsAsync(user);

            var controller = new UsersController(_context, _userManagerMock.Object);

            // Act
            var result = await controller.Details("1") as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user, result.Model);
        }

        [Fact]
        public async Task Details_ReturnsNotFound_WhenUserMissing()
        {
            // Arrange
            _userManagerMock.Setup(x => x.FindByIdAsync("99"))
                .ReturnsAsync((User)null);

            var controller = new UsersController(_context, _userManagerMock.Object);

            // Act
            var result = await controller.Details("99");

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteConfirmed_RemovesUserAndRedirects()
        {
            // Arrange
            var user = new User { Id = "1", Email = "delete@example.com" };
            _userManagerMock.Setup(x => x.FindByIdAsync("1"))
                .ReturnsAsync(user);
            _userManagerMock.Setup(x => x.DeleteAsync(user))
                .ReturnsAsync(IdentityResult.Success);

            var controller = new UsersController(_context, _userManagerMock.Object);

            // Act
            var result = await controller.DeleteConfirmed("1") as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            _userManagerMock.Verify(x => x.DeleteAsync(user), Times.Once);
        }
    }
}
